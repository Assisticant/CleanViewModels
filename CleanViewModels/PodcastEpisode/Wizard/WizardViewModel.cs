using CleanViewModels.PodcastEpisode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using CleanViewModels.PodcastEpisode.Services;
using CleanViewModels.PodcastEpisode.Repositories;
using CleanViewModels.MVVM;
using System.Windows.Input;
using System.ComponentModel;
namespace CleanViewModels.PodcastEpisode.Wizard
{
    public class WizardViewModel : ViewModelBase
    {
        private readonly IUploadService _uploadService;

        private readonly TitleViewModel _title;
        private readonly FileViewModel _file;
        private readonly UrlViewModel _url;
        private readonly ReviewViewModel _review;

        private readonly RelayCommand _goBack;
        private readonly RelayCommand _goForward;
        private readonly RelayCommand _finish;
        private readonly RelayCommand _cancel;
        
        private object _currentPage;

        public event EventHandler Closed;

        public WizardViewModel(
            IGenreRepository genreRepository,
            IUploadService uploadService)
        {
            _uploadService = uploadService;

            _title = new TitleViewModel(genreRepository);
            _file = new FileViewModel();
            _url = new UrlViewModel();
            _review = new ReviewViewModel();

            _title.PropertyChanged += PagePropertyChanged;
            _file.PropertyChanged += PagePropertyChanged;
            _url.PropertyChanged += PagePropertyChanged;

            _goBack = new RelayCommand(DoGoBack);
            _goForward = new RelayCommand(DoGoForward);
            _finish = new RelayCommand(DoFinish);
            _cancel = new RelayCommand(DoCancel);

            _currentPage = _title;
        }

        public async Task LoadAsync()
        {
            await _title.LoadAsync();
        }

        public object CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ICommand GoBack
        {
            get { return _goBack; }
        }

        public ICommand GoForward
        {
            get { return _goForward; }
        }

        public ICommand Finish
        {
            get { return _finish; }
        }

        public ICommand Cancel
        {
            get { return _cancel; }
        }

        private void DoGoBack()
        {
            var currentPage = CurrentPage;
            object priorPage = null;
            if (currentPage == _file || CurrentPage == _url)
                priorPage = _title;
            else if (currentPage == _review)
            {
                if (_title.ArtworkSource == 1)
                    priorPage = _file;
                else if (_title.ArtworkSource == 2)
                    priorPage = _url;
                else
                    priorPage = _title;
            }
            CurrentPage = priorPage;
            UpdateButtons();
        }

        private void DoGoForward()
        {
            var currentPage = CurrentPage;
            object nextPage = null;
            if (currentPage == _title)
            {
                if (_title.ArtworkSource == 1)
                    nextPage = _file;
                else if (_title.ArtworkSource == 2)
                    nextPage = _url;
                else
                    nextPage = _review;
            }
            if (currentPage == _file || CurrentPage == _url)
                nextPage = _review;
            CurrentPage = nextPage;
            UpdateButtons();
        }

        private async void DoFinish()
        {
            Uri artworkUrl;
            if (!Uri.TryCreate(_url.ArtworkUrl, UriKind.Absolute, out artworkUrl))
                artworkUrl = null;
            var upload = new Upload()
            {
                Title = _title.Title,
                Genre = _title.Genre,
                ArtworkSource =
                    _title.ArtworkSource == 1 ? ArtworkSource.File :
                    _title.ArtworkSource == 2 ? ArtworkSource.Url :
                    ArtworkSource.None,
                ArtworkFile = _file.ArtworkFileName,
                ArtworkUrl = artworkUrl
            };
            await _uploadService.UploadAsync(upload);
            if (Closed != null)
                Closed(this, new EventArgs());
        }

        private void DoCancel()
        {
            if (Closed != null)
                Closed(this, new EventArgs());
        }

        private void PagePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateButtons();

            if (e.PropertyName == "Title")
                _review.Title = _title.Title;
            else if (e.PropertyName == "Genre")
                _review.Genre = _title.Genre == null ? "Not Selected" :
                    _title.Genre.Name;
            else if (e.PropertyName == "ArtworkFileName")
                _review.ArtworkFileName = _file.ArtworkFileName;
            else if (e.PropertyName == "ArtworkUrl")
                _review.ArtworkUrl = _url.ArtworkUrl;
            else if (e.PropertyName == "ArtworkSource")
            {
                _review.IsArtworkFile = _title.ArtworkSource == 1;
                _review.IsArtworkUrl = _title.ArtworkSource == 2;
            }
        }

        private void UpdateButtons()
        {
            _goBack.SetCanExecute(CurrentPage != _title);
            _goForward.SetCanExecute(CurrentPage != _review);
            _finish.SetCanExecute(
                !string.IsNullOrWhiteSpace(_title.Title) &&
                _title.Genre != null &&
                (_title.ArtworkSource == 1 ?
                    !string.IsNullOrWhiteSpace(_file.ArtworkFileName) :
                    _title.ArtworkSource == 2 ?
                    !string.IsNullOrWhiteSpace(_url.ArtworkUrl) :
                    true));
        }
    }
}
