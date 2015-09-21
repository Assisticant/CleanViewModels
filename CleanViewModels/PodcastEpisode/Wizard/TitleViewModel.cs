using CleanViewModels.PodcastEpisode.Models;
using CleanViewModels.PodcastEpisode.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanViewModels.PodcastEpisode.Wizard
{
    public class TitleViewModel : IPage
    {
        private readonly Upload _upload;
        private readonly IGenreRepository _genreRepository;
        private readonly Func<Genre, GenreViewModel> _makeGenreViewModel;
        
        public TitleViewModel(
            Upload upload, 
            IGenreRepository genreRepository, 
            Func<Genre, GenreViewModel> makeGenreViewModel)
        {
            _upload = upload;
            _genreRepository = genreRepository;
            _makeGenreViewModel = makeGenreViewModel;
        }

        public bool Active
        {
            get { return true; }
        }

        public string Title
        {
            get { return _upload.Title; }
            set { _upload.Title = value; }
        }

        public GenreViewModel Genre
        {
            get
            {
                return _upload.Genre == null ? null :
                    _makeGenreViewModel(_upload.Genre);
            }
            set
            {
                _upload.Genre = value == null ? null :
                    value.Genre;
            }
        }

        public ImmutableList<GenreViewModel> Genres
        {
            get
            {
                var genres =
                    from genre in _genreRepository.Genres
                    orderby genre.Name
                    select _makeGenreViewModel(genre);
                return genres.ToImmutableList();
            }
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
