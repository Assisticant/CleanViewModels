using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanViewModels.PodcastEpisode.Models;

namespace CleanViewModels.PodcastEpisode.Wizard
{
    public class FileViewModel
    {
        private readonly Upload _upload;

        public FileViewModel(Upload upload)
        {
            _upload = upload;
        }

        public string ArtworkFileName
        {
            get { return _upload.ArtworkFile; }
            set { _upload.ArtworkFile = value; }
        }
    }
}
