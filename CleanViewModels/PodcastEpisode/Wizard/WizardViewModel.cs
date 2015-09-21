using Assisticant.Fields;
using CleanViewModels.PodcastEpisode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using CleanViewModels.PodcastEpisode.Services;

namespace CleanViewModels.PodcastEpisode.Wizard
{
    public class WizardViewModel
    {
        private readonly IUploadService _uploadService;

        private readonly Upload _upload;
        private readonly TitleViewModel _title;
        private readonly FileViewModel _file;
        private readonly UrlViewModel _url;
        private readonly ReviewViewModel _review;

        private Observable<object> _currentPage = new Observable<object>();
        
        public event EventHandler Closed;

        public WizardViewModel(
            Upload upload,
            IUploadService uploadService,
            Func<Upload, TitleViewModel> makeTitle,
            Func<Upload, FileViewModel> makeFile,
            Func<Upload, UrlViewModel> makeUrl,
            Func<Upload, ReviewViewModel> makeReview)
        {
            _upload = upload;
            _uploadService = uploadService;
            _title = makeTitle(upload);
            _file = makeFile(upload);
            _url = makeUrl(upload);
            _review = makeReview(upload);

            _currentPage.Value = _title;
        }

        public async Task LoadAsync()
        {
            await _title.LoadAsync();
        }

        public object CurrentPage
        {
            get { return _currentPage.Value; }
        }

        public bool CanGoBack
        {
            get { return _currentPage.Value != _title; }
        }

        public void GoBack()
        {
            Contract.Requires(CanGoBack);

            var currentPage = _currentPage.Value;
            object priorPage = null;
            if (currentPage == _file || CurrentPage == _url)
                priorPage = _title;
            else if (currentPage == _review)
            {
                if (_upload.ArtworkSource == ArtworkSource.File)
                    priorPage = _file;
                else if (_upload.ArtworkSource == ArtworkSource.Url)
                    priorPage = _url;
                else
                    priorPage = _title;
            }
            _currentPage.Value = priorPage;
        }

        public bool CanGoForward
        {
            get { return _currentPage.Value != _review; }
        }

        public void GoForward()
        {
            Contract.Requires(CanGoForward);

            var currentPage = _currentPage.Value;
            object nextPage = null;
            if (currentPage == _title)
            {
                if (_upload.ArtworkSource == ArtworkSource.File)
                    nextPage = _file;
                else if (_upload.ArtworkSource == ArtworkSource.Url)
                    nextPage = _url;
                else
                    nextPage = _review;
            }
            if (currentPage == _file || CurrentPage == _url)
                nextPage = _review;
            _currentPage.Value = nextPage;
        }

        public bool CanFinish
        {
            get
            {
                return
                    !string.IsNullOrWhiteSpace(_upload.Title) &&
                    _upload.Genre != null &&
                    (_upload.ArtworkSource == ArtworkSource.File ?
                        !string.IsNullOrWhiteSpace(_upload.ArtworkFile) :
                     _upload.ArtworkSource == ArtworkSource.Url ?
                        _upload.ArtworkUrl != null :
                        true);
            }
        }

        public async void Finish()
        {
            Contract.Requires(CanFinish);

            await _uploadService.UploadAsync(_upload);
            if (Closed != null)
                Closed(this, new EventArgs());
        }

        public void Cancel()
        {
            if (Closed != null)
                Closed(this, new EventArgs());
        }
    }
}
