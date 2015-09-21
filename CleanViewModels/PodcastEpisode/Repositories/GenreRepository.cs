using CleanViewModels.PodcastEpisode.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanViewModels.PodcastEpisode.Repositories
{
    class GenreRepository : IGenreRepository
    {
        private ObservableCollection<Genre> _genres = new ObservableCollection<Genre>();

        public ObservableCollection<Genre> Genres
        {
            get
            {
                if (!_genres.Any())
                    LoadAsync();
                return _genres;
            }
        }

        private async void LoadAsync()
        {
            //await Task.Delay(3000);
            _genres.Clear();
            _genres.Add(new Genre(1) { Name = "Comedy" });
            _genres.Add(new Genre(2) { Name = "Technology" });
            _genres.Add(new Genre(3) { Name = "Science" });
            _genres.Add(new Genre(4) { Name = "Business" });
        }
    }
}
