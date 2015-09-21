using CleanViewModels.MVVM;
using CleanViewModels.PodcastEpisode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanViewModels.PodcastEpisode.Wizard
{
    public class ReviewViewModel : ViewModelBase
    {
        private string _title;
        private string _genre;
        private bool _isArtworkFile;
        private string _artworkFileName;
        private bool _isArtworkUrl;
        private string _artworkUrl;

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Genre
        {
            get { return _genre; }
            set
            {
                if (_genre != value)
                {
                    _genre = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool IsArtworkFile
        {
            get { return _isArtworkFile; }
            set
            {
                if (_isArtworkFile != value)
                {
                    _isArtworkFile = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string ArtworkFileName
        {
            get { return _artworkFileName; }
            set
            {
                if (_artworkFileName != value)
                {
                    _artworkFileName = value;
                    RaisePropertyChanged();
                }
            }
        }
        public bool IsArtworkUrl
        {
            get { return _isArtworkUrl; }
            set
            {
                if (_isArtworkFile != value)
                {
                    _isArtworkUrl = value;
                    RaisePropertyChanged();
                }
            }
        }

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
