using Assisticant.Fields;
using CleanViewModels.PodcastEpisode.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanViewModels.PodcastEpisode.Models
{
    public class Upload
    {
        private readonly IUploadService _uploadService;

        private Observable<string> _title = new Observable<string>();
        private Observable<Genre> _genre = new Observable<Genre>();
        private Observable<ArtworkSource> _artworkSource = new Observable<ArtworkSource>();
        private Observable<string> _artworkFile = new Observable<string>();
        private Observable<Uri> _artworkUrl = new Observable<Uri>();
        
        public Upload(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        public string Title
        {
            get { return _title; }
            set { _title.Value = value; }
        }

        public Genre Genre
        {
            get { return _genre; }
            set { _genre.Value = value; }
        }

        public ArtworkSource ArtworkSource
        {
            get { return _artworkSource; }
            set { _artworkSource.Value = value; }
        }

        public string ArtworkFile
        {
            get { return _artworkFile; }
            set { _artworkFile.Value = value; }
        }

        public Uri ArtworkUrl
        {
            get { return _artworkUrl; }
            set { _artworkUrl.Value = value; }
        }

        public async Task ExecuteAsync()
        {
            await _uploadService.UploadAsync(this);
        }
    }
}
