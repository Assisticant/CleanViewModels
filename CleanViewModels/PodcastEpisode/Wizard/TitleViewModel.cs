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
    public class TitleViewModel
    {
        private readonly Upload _upload;
        private readonly IGenreRepository _genreRepository;
        
        public TitleViewModel(
            Upload upload, 
            IGenreRepository genreRepository)
        {
            _upload = upload;
            _genreRepository = genreRepository;
        }

        public string Title
        {
            get { return _upload.Title; }
            set { _upload.Title = value; }
        }

        public Genre Genre
        {
            get { return _upload.Genre; }
            set { _upload.Genre = value; }
        }

        public ObservableCollection<Genre> Genres
        {
            get { return _genreRepository.Genres; }
        }

        public int ArtworkSource
        {
            get
            {
                if (_upload.ArtworkSource == Models.ArtworkSource.File)
                    return 1;
                else if (_upload.ArtworkSource == Models.ArtworkSource.Url)
                    return 2;
                else
                    return 0;
            }
            set
            {
                if (value == 1)
                    _upload.ArtworkSource = Models.ArtworkSource.File;
                else if (value == 2)
                    _upload.ArtworkSource = Models.ArtworkSource.Url;
                else
                    _upload.ArtworkSource = 0;
            }
        }
    }
}
