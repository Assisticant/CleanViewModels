using Assisticant.Fields;
using CleanViewModels.PodcastEpisode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
namespace CleanViewModels.PodcastEpisode.Wizard
{
    public class WizardViewModel
    {
        private readonly Upload _upload;
        private readonly IPage[] _pages;

        private Observable<int> _currentPageIndex = new Observable<int>();
        
        public event DialogClosedHandler Closed;

        public WizardViewModel(
            Upload upload,
            Func<Upload, TitleViewModel> makeTitle,
            Func<Upload, FileViewModel> makeFile,
            Func<Upload, UrlViewModel> makeUrl,
            Func<Upload, ReviewViewModel> makeReview)
        {
            _upload = upload;
            _pages = new IPage[]
            {
                makeTitle(upload),
                makeFile(upload),
                makeUrl(upload),
                makeReview(upload)
            };
        }

        public IPage CurrentPage
        {
            get { return _pages[_currentPageIndex]; }
        }

        public bool CanGoBack
        {
            get { return _pages.Take(_currentPageIndex).Any(p => p.Active); }
        }

        public void GoBack()
        {
            Contract.Requires(CanGoBack);

            int index = _currentPageIndex - 1;
            while (!_pages[index].Active)
                index--;
            _currentPageIndex.Value = index;
        }

        public bool CanGoForward
        {
            get { return _pages.Skip(_currentPageIndex + 1).Any(p => p.Active); }
        }

        public void GoForward()
        {
            Contract.Requires(CanGoForward);

            int index = _currentPageIndex + 1;
            while (!_pages[index].Active)
                index++;
            _currentPageIndex.Value = index;
        }

        public bool CanFinish
        {
            get { return _upload.IsComplete; }
        }

        public void Finish()
        {
            Contract.Requires(CanFinish);

            if (Closed != null)
                Closed(this, new DialogClosedEventArgs { Finished = true });
        }

        public void Cancel()
        {
            if (Closed != null)
                Closed(this, new DialogClosedEventArgs { Finished = false });
        }
    }
}
