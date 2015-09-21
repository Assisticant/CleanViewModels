using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CleanViewModels.PodcastEpisode.Services;
using CleanViewModels.PodcastEpisode.Models;
using System.Threading.Tasks;

namespace CleanViewModels.Tests
{
    public class FakeUploadService : IUploadService
    {
        public Task UploadAsync(Upload upload)
        {
            throw new NotImplementedException();
        }
    }
}
