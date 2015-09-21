using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanViewModels.PodcastEpisode.Models;

namespace CleanViewModels.PodcastEpisode.Wizard
{
    public class FileViewModel : IPage
    {
        private readonly Upload _upload;

        public FileViewModel(Upload upload)
        {
            _upload = upload;
        }

        public bool Active
        {
            get { return _upload.ArtworkSource == ArtworkSource.File; }
        }

        public string ArtworkFileName
        {
            get { return _upload.ArtworkFile; }
            set { _upload.ArtworkFile = value; }
        }
    }
}
