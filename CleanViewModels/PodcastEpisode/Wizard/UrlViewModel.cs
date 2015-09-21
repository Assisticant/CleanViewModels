using CleanViewModels.PodcastEpisode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanViewModels.PodcastEpisode.Wizard
{
    public class UrlViewModel : IPage
    {
        private readonly Upload _upload;

        public UrlViewModel(Upload upload)
        {
            _upload = upload;
        }

        public bool Active
        {
            get { return _upload.ArtworkSource == ArtworkSource.Url; }
        }

        public string ArtworkUrl
        {
            get
            {
                return _upload.ArtworkUrl == null ? "" :
                    _upload.ArtworkUrl.AbsoluteUri;
            }
            set
            {
                Uri uri;
                if (Uri.TryCreate(value, UriKind.Absolute, out uri))
                    _upload.ArtworkUrl = uri;
            }
        }
    }
}
