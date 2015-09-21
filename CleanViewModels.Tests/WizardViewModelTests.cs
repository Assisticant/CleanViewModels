using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CleanViewModels.PodcastEpisode.Wizard;
using CleanViewModels.PodcastEpisode.Models;
using FluentAssertions;

namespace CleanViewModels.Tests
{
    [TestClass]
    public class WizardViewModelTests
    {
        [TestMethod]
        public void CanFinishAfterTitleAndGenre()
        {
            var viewModel = GivenMinimalViewModel();
            viewModel.CanFinish.Should().BeTrue();
        }

        [TestMethod]
        public void TitleIsRequired()
        {
            var viewModel = GivenMinimalViewModel();
            ((TitleViewModel)viewModel.CurrentPage).Title = null;
            viewModel.CanFinish.Should().BeFalse();
        }

        [TestMethod]
        public void GenreIsRequired()
        {
            var viewModel = GivenMinimalViewModel();
            ((TitleViewModel)viewModel.CurrentPage).Genre = null;
            viewModel.CanFinish.Should().BeFalse();
        }

        [TestMethod]
        public void ReviewPageIsNextByDefault()
        {
            var viewModel = GivenMinimalViewModel();
            viewModel.GoForward();
            viewModel.CurrentPage.Should().BeOfType<ReviewViewModel>();
        }

        [TestMethod]
        public void FilePageIsShownIfFileIsSelected()
        {
            var viewModel = GivenMinimalViewModel();
            ((TitleViewModel)viewModel.CurrentPage).ArtworkSource = 1; // File
            viewModel.GoForward();
            viewModel.CurrentPage.Should().BeOfType<FileViewModel>();
        }

        [TestMethod]
        public void UrlPageIsShownIfUrlIsSelected()
        {
            var viewModel = GivenMinimalViewModel();
            ((TitleViewModel)viewModel.CurrentPage).ArtworkSource = 2; // Url
            viewModel.GoForward();
            viewModel.CurrentPage.Should().BeOfType<UrlViewModel>();
        }

        [TestMethod]
        public void FileNameIsRequiredIfFileIsSelected()
        {
            var viewModel = GivenMinimalViewModel();
            ((TitleViewModel)viewModel.CurrentPage).ArtworkSource = 1; // File
            viewModel.CanFinish.Should().BeFalse();
        }

        [TestMethod]
        public void CanFinishIfFileNameIsSupplied()
        {
            var viewModel = GivenMinimalViewModel();
            ((TitleViewModel)viewModel.CurrentPage).ArtworkSource = 1; // File
            viewModel.GoForward();
            ((FileViewModel)viewModel.CurrentPage).ArtworkFileName = "C:\artwork.jpg";
            viewModel.CanFinish.Should().BeTrue();
        }

        [TestMethod]
        public void UrlIsRequiredIfUrlIsSelected()
        {
            var viewModel = GivenMinimalViewModel();
            ((TitleViewModel)viewModel.CurrentPage).ArtworkSource = 2; // Url
            viewModel.CanFinish.Should().BeFalse();
        }

        [TestMethod]
        public void CanFinishIfUrlIsSupplied()
        {
            var viewModel = GivenMinimalViewModel();
            ((TitleViewModel)viewModel.CurrentPage).ArtworkSource = 2; // Url
            viewModel.GoForward();
            ((UrlViewModel)viewModel.CurrentPage).ArtworkUrl = "http://localhost/artwork.jpg";
            viewModel.CanFinish.Should().BeTrue();
        }

        private WizardViewModel GivenMinimalViewModel()
        {
            var fakeUploadService = new FakeUploadService();
            var upload = new Upload();
            var fakeGenreRepository = new FakeGenreRepository();
            var viewModel = new WizardViewModel(upload,
                fakeUploadService,
                u => new TitleViewModel(u, fakeGenreRepository),
                u => new FileViewModel(u),
                u => new UrlViewModel(u),
                u => new ReviewViewModel(u));
            ((TitleViewModel)viewModel.CurrentPage).Title = "Test";
            ((TitleViewModel)viewModel.CurrentPage).Genre = new Genre(0);
            return viewModel;
        }
    }
}
