using CleanViewModels.PodcastEpisode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanViewModels.PodcastEpisode.Wizard
{
    public class ReviewViewModel : IPage
    {
        private readonly Upload _upload;

        public ReviewViewModel(Upload upload)
        {
            _upload = upload;
        }

        public bool Active
        {
            get { return true; }
        }

        public string Title
        {
            get { return _upload.Title; }
        }

        public string Genre
        {
            get { return _upload.Genre == null ? "Not selected" : _upload.Genre.Name; }
        }

        public bool IsArtworkFile
        {
            get { return _upload.ArtworkSource == ArtworkSource.File; }
        }

        public string ArtworkFileName
        {
            get { return _upload.ArtworkFile; }
        }

        public bool IsArtworkUrl
        {
            get { return _upload.ArtworkSource == ArtworkSource.Url; }
        }

        public string ArtworkUrl
        {
            get { return _upload.ArtworkUrl == null ? "Not selected" : _upload.ArtworkUrl.AbsoluteUri; }
        }
    }
}
