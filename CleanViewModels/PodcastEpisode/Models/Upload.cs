using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanViewModels.PodcastEpisode.Models
{
    public class Upload
    {
        private Observable<string> _title = new Observable<string>();
        private Observable<Genre> _genre = new Observable<Genre>();
        private Observable<ArtworkSource> _artworkSource = new Observable<ArtworkSource>();
        private Observable<string> _artworkFile = new Observable<string>();
        private Observable<Uri> _artworkUrl = new Observable<Uri>();

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

        public bool IsComplete
        {
            get
            {
                return
                    !string.IsNullOrWhiteSpace(Title) &&
                    Genre != null &&
                    (ArtworkSource == ArtworkSource.File ?
                        !string.IsNullOrWhiteSpace(ArtworkFile) :
                     ArtworkSource == ArtworkSource.Url ?
                        ArtworkUrl != null :
                        true);
            }
        }
    }
}
