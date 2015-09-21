using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanViewModels.PodcastEpisode.Wizard
{
    public interface IPage
    {
        bool Active { get; }
    }
}
