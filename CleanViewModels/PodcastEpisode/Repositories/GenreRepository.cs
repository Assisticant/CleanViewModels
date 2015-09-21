using Assisticant.Collections;
using CleanViewModels.PodcastEpisode.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanViewModels.PodcastEpisode.Repositories
{
    class GenreRepository : IGenreRepository
    {
        private ObservableList<Genre> _genres = new ObservableList<Genre>();

        public ImmutableList<Genre> Genres
        {
            get
            {
                lock (this)
                {
                    return _genres.ToImmutableList();
                }
            }
        }

        public async Task LoadAsync()
        {
            await Task.Delay(3000);
            lock (this)
            {
                _genres.Clear();
                _genres.Add(new Genre(1) { Name = "Comedy" });
                _genres.Add(new Genre(2) { Name = "Technology" });
                _genres.Add(new Genre(3) { Name = "Science" });
                _genres.Add(new Genre(4) { Name = "Business" });
            }
        }
    }
}
