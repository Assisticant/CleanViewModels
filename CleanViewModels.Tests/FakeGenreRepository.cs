using CleanViewModels.PodcastEpisode.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CleanViewModels.PodcastEpisode.Models;
using System.Collections.ObjectModel;

namespace CleanViewModels.Tests
{
    public class FakeGenreRepository : IGenreRepository
    {
        public ObservableCollection<Genre> Genres
        {
            get { throw new NotImplementedException(); }
        }
    }
}
