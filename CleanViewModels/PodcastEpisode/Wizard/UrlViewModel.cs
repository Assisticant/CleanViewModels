using CleanViewModels.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanViewModels.PodcastEpisode.Wizard
{
    public class UrlViewModel : ViewModelBase
    {
        private string _artworkUrl;

        public string ArtworkUrl
        {
            get { return _artworkUrl; }
            set
            {
                if (_artworkUrl != value)
                {
                    _artworkUrl = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
