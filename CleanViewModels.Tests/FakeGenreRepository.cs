using CleanViewModels.PodcastEpisode.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CleanViewModels.PodcastEpisode.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CleanViewModels.Tests
{
    public class FakeGenreRepository : IGenreRepository
    {
        public async Task LoadAsync()
        {
            throw new NotImplementedException();
        }
        public ObservableCollection<Genre> Genres
        {
            get { throw new NotImplementedException(); }
        }
    }
}
