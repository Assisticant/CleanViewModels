using CleanViewModels.PodcastEpisode.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Immutable;
using CleanViewModels.PodcastEpisode.Models;

namespace CleanViewModels.Tests
{
    public class FakeGenreRepository : IGenreRepository
    {
        public ImmutableList<Genre> Genres
        {
            get { throw new NotImplementedException(); }
        }
    }
}
