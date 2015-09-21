using CleanViewModels.PodcastEpisode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanViewModels.PodcastEpisode.Wizard
{
    public class GenreViewModel
    {
        private readonly Genre _genre;

        public GenreViewModel(Genre genre)
        {
            _genre = genre;            
        }

        public Genre Genre
        {
            get { return _genre; }
        }

        public string Name
        {
            get { return _genre.Name; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return _genre.Id == ((GenreViewModel)obj)._genre.Id;
        }

        public override int GetHashCode()
        {
            return _genre.Id;
        }
    }
}
