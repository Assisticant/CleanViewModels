using CleanViewModels.PodcastEpisode.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CleanViewModels.PodcastEpisode.Repositories
{
    public interface IGenreRepository
    {
        Task LoadAsync();
        ObservableCollection<Genre> Genres { get; }
    }
}
