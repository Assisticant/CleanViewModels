using CleanViewModels.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanViewModels.PodcastEpisode.Wizard
{
    public class FileViewModel : ViewModelBase
    {
        private string _artworkFile;

        public string ArtworkFileName
        {
            get { return _artworkFile; }
            set
            {
                if (_artworkFile != value)
                {
                    _artworkFile = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
