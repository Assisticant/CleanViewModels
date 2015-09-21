using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CleanViewModels.PodcastEpisode.Models;
using FluentAssertions;

namespace CleanViewModels.Tests
{
    [TestClass]
    public class UploadTests
    {
        [TestMethod]
        public void UploadRequiresTitle()
        {
            var upload = GivenMinimumUpload();
            upload.Title = null;
            upload.IsComplete.Should().BeFalse();
        }

        [TestMethod]
        public void UploadRequiresGenre()
        {
            var upload = GivenMinimumUpload();
            upload.Genre = null;
            upload.IsComplete.Should().BeFalse();
        }

        [TestMethod]
        public void UploadWithTitleAndGenreIsComplete()
        {
            var upload = GivenMinimumUpload();
            upload.IsComplete.Should().BeTrue();
        }

        [TestMethod]
        public void UploadRequiresFileNameIfFileIsSelected()
        {
            var upload = GivenMinimumUpload();
            upload.ArtworkSource = ArtworkSource.File;
            upload.IsComplete.Should().BeFalse();
        }

        [TestMethod]
        public void UploadWithFileNameIsComplete()
        {
            var upload = GivenMinimumUpload();
            upload.ArtworkSource = ArtworkSource.File;
            upload.ArtworkFile = "c:\artwork.jpg";
            upload.IsComplete.Should().BeTrue();
        }

        [TestMethod]
        public void UploadRequiresUrlIfUrlIsSelected()
        {
            var upload = GivenMinimumUpload();
            upload.ArtworkSource = ArtworkSource.Url;
            upload.IsComplete.Should().BeFalse();
        }

        [TestMethod]
        public void UploadWithUrlIsComplete()
        {
            var upload = GivenMinimumUpload();
            upload.ArtworkSource = ArtworkSource.Url;
            upload.ArtworkUrl = new Uri("http://localhost/artwork.jpg", UriKind.Absolute);
            upload.IsComplete.Should().BeTrue();
        }

        private static Upload GivenMinimumUpload()
        {
            Upload upload = new Upload();
            upload.Title = "Test";
            upload.Genre = new Genre(0);
            return upload;
        }
    }
}
