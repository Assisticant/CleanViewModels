using CleanViewModels.PodcastEpisode.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanViewModels.PodcastEpisode.Models
{
    public class Upload
    {
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public ArtworkSource ArtworkSource { get; set; }
        public string ArtworkFile { get; set; }
        public Uri ArtworkUrl { get; set; }
    }
}
