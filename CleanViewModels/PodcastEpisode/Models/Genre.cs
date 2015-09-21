using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanViewModels.PodcastEpisode.Models
{
    public class Genre
    {
        private readonly int _id;

        public Genre(int id)
        {
            _id = id;
        }

        public int Id
        {
            get { return _id; }
        }

        public string Name { get; set; }
    }
}
