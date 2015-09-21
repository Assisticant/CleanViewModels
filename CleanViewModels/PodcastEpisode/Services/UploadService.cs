using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanViewModels.PodcastEpisode.Models;

namespace CleanViewModels.PodcastEpisode.Services
{
    class UploadService : IUploadService
    {
        public async Task UploadAsync(Upload upload)
        {
            await Task.Delay(5000);
            //throw new ApplicationException("Failed to upload");
        }
    }
}
