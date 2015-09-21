# CleanViewModels
Example of applying the SOLID principles to clean a view model.

Step through the phases of cleaning up a wizard view model. Each phase has its own branch, so you can see
exactly what changed at each step.

## Start

The story begins with a wizard view model implemented using a traditional MVVM approach. The `WizardViewModel` has several pages:

* `TitleViewModel`
* `FileViewModel`
* `UrlViewModel`
* `ReviewViewModel`

Each of these view models inherits from a base class that implements `INotifyPropertyChanged`. The `WizardViewModel` uses
`RelayCommand`s to operate four buttons. It determines which view model to display when the user clicks Back and Next.
It also determines when the user has finished entering all of the data, so that it can activate the Finish button.

The `WizardViewModel` calls the `IUploadService` to upload the podcast when the user clicks Finish. The `TitleViewModel` calls 
the `GenreRepository` to load its list of genres.

Notice how the `WizardViewModel` subscribes to the `PropertyChanged` event on its child pages so that it can update the preview
page. It has to, because each page has a separate copy of the data. If it didn't use `PropertyChanged`, then it would have to
use a message bus or something similar to achieve the same result.

## Assisticant

When we transition to Assisticant, we change all of the mutable properties to Observables. This means that we don't have to
keep separate copies of the data in each view model. Instead, they all reference the same `Upload` object, which eliminates
the need to subscribe to changes to keep each other in sync. We get to delete that code from `WizardViewModel`.

The `WizardViewModel` also starts to take advantage of contention-based `ICommand` implementations. Instead of creating
`RelayCommand`s, it implements properties like `CanGoBack` to determine when the user can click the Back button. And it
implements methods like `Back` to execute that command.

## SRP1

The `WizardViewModel` has far too many responsibilities. It projects the model into a form that can be bound to the view.
But it also calls the `IUploadService` to upload the poscast. If the view changes, we have to change the view model. And if
the service API changes, we also have to change the view model. The view model has two reasons to change.

We move this extra responsibility out of the view model and into the model. This makes things a little better, but it's not
good enough. Now the model is taking on too much responsibility. And some of that responsibility is asynchronous, which the
model is not equiped to handle. Notice the proliferation of `async void` methods.

## SRP2

Rather than just shifting responsibilities, let's introduce a whole new class to manage service calls. The `ServiceManager`
takes on the responsibilities of calling services. It does so in a responsible asynchronous fashion. It keeps a busy
indicator, and catches any exceptions that might happen.

To show the user the busy indicator and the exception, we added a `MainViewModel`. This gets injected an `IServiceStatus`
interface, which the `ServiceManager` implements for us. Because this status is observable, we can data bind to these
properties to see when a call is in progress, or when an error has occurred.

## ISP

There are a couple of places where the view model exposes more than it should. Let's segregate those interfaces so that
the client doesn't have access to any methods that it shouldn't use.

The `GenreViewModel` has so far been returning an `ObservableCollection<Genre>`. That means that a caller could add or
remove objects from that collection, or change the name of one of the genres. This observable collection is in the
`GenreRepository` itself. Let's nip that in the bud by first having the repository convert the collection to an
`ImmutableList<Genre>` before returning it.

But that's not quite enough. We also want to limit the interface by not returning a full `Genre` object from the
`TitleViewModel`. Let's wrap each one in a `GenreViewModel` that provides just the surface area that the view needs.
Since names are read-only on this particular view, they should be getter-only properties.

We'll take advantage of this opportunity to order the list of genres by name. We do this by adding a single `orderby`
clause to the linq statement.

## OCP

Now we can tackle the elephant in the room. The `WizardViewModel` is not **open** to extension and **closed** for
modification. It knows far too much about the pages that it contains. It orchestrates the navigation from page to page.
Let's move some of that intelligence into the pages themselves.

Each page implements a new `IPage` interface, which exposes an `Active` property. In its implementation, it determines
when that page is an active part of the navigation flow. The wizard view model will simply transition to the next or
previous active page.

Next, we notice that the wizard view model is also quite knowledegable about when the user has filled in all of the
necessary fields of the upload. Rather than owning that domain logic itself, we'll let it delegate to the `Upload` object.

Now if we need to add features to the upload, we can do so without modifying the `WizardViewModel`. It simply delegates
to the pages to see if they are active for navigation, and to the `Upload` to see if it is complete. We can extend the
behavior of the wizard without modifying this class.

At this point, we can also simplify our tests quite a bit. Before, we had to test the `WizardViewModel`, because it had
all of the domain knowledge. That means that we also had to mock all of its dependencies. However, now that we've
offloaded that domain knowledge, we can simply test the `Upload`. It has no dependencies, so it's really easy to test.

## Conclusion

Over several iterations, we have cleaned up this view model and its children. We started with all of the data stored
inside the view models. They had to do a lot of coordination to get the right behavior. Moving to Assisticant allowed us
to shift that data into a shared domain object.

With that extra freedom, we gradually refactored to move responsibilities away from the wizard view model. We moved the
responsibility of calling services to a service manager. And we moved the responsibilities of determining page navigation
to the pages themselves. Finally, we moved the responsibility of determining when the domain object was completely filled
out into the domain object where it belongs.

The result is a cleaner set of view models. These are easier to read, easier to test, and easier to modify.
