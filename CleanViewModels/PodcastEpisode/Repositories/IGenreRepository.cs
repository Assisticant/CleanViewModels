using CleanViewModels.PodcastEpisode.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CleanViewModels.PodcastEpisode.Repositories
{
    public interface IGenreRepository
    {
        ObservableCollection<Genre> Genres { get; }
    }
}
