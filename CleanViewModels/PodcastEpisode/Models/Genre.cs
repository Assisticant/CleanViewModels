using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanViewModels.PodcastEpisode.Models
{
    public class Genre
    {
        private readonly int _id;
        private Observable<string> _name = new Observable<string>();

        public Genre(int id)
        {
            _id = id;
        }

        public int Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
            set { _name.Value = value; }
        }
    }
}
