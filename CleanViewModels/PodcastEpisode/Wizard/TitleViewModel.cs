using CleanViewModels.MVVM;
using CleanViewModels.PodcastEpisode.Models;
using CleanViewModels.PodcastEpisode.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanViewModels.PodcastEpisode.Wizard
{
    public class TitleViewModel : ViewModelBase
    {
        private readonly IGenreRepository _genreRepository;

        private string _title;
        private Genre _genre;
        private int _artworkSource;

        public TitleViewModel(
            IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task LoadAsync()
        {
            await _genreRepository.LoadAsync();
        }

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

        public Genre Genre
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

        public ObservableCollection<Genre> Genres
        {
            get { return _genreRepository.Genres; }
        }

        public int ArtworkSource
        {
            get { return _artworkSource; }
            set
            {
                if (_artworkSource != value)
                {
                    _artworkSource = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
