using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanViewModels.PodcastEpisode.Models;

namespace CleanViewModels.PodcastEpisode.Services
{
    public interface IUploadService
    {
        Task UploadAsync(Upload upload);
    }
}
