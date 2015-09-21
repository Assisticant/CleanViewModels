using CleanViewModels.PodcastEpisode.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace CleanViewModels.PodcastEpisode.Repositories
{
    public interface IGenreRepository
    {
        ImmutableList<Genre> Genres { get; }
    }
}
