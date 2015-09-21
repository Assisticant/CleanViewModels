using Assisticant;
using Assisticant.Fields;
using CleanViewModels.PodcastEpisode.Models;
using CleanViewModels.PodcastEpisode.Repositories;
using CleanViewModels.PodcastEpisode.Services;
using CleanViewModels.PodcastEpisode.Wizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanViewModels
{
    class ServiceManager : IServiceStatus
    {
        private readonly GenreRepository _genreRepository;
        private readonly Func<Upload, WizardViewModel> _makeWizardViewModel;
        private readonly IUploadService _uploadService;

        private Observable<bool> _isBusy = new Observable<bool>();
        private Observable<string> _lastError = new Observable<string>();
        
        public ServiceManager(
            GenreRepository genreRepository,
            Func<Upload, WizardViewModel> makeWizardViewModel,
            IUploadService uploadService)
        {
            _genreRepository = genreRepository;
            _makeWizardViewModel = makeWizardViewModel;
            _uploadService = uploadService;
        }

        public void RunWizard()
        {
            // Start asynchronously loading what the dialog will need.
            Perform(() => _genreRepository.LoadAsync());

            // Create the model and inject it into the view models.
            var upload = new Upload();
            var viewModel = _makeWizardViewModel(upload);

            // Show the dialog with the view models.
            var dialog = new WizardDialog();
            dialog.DataContext = ForView.Wrap(viewModel);
            if (dialog.ShowDialog() ?? false)
            {
                // Call the service.
                Perform(() => _uploadService.UploadAsync(upload));
            }
        }

        public bool IsBusy
        {
            get
            {
                lock (this)
                {
                    return _isBusy;
                }
            }
        }

        public string LastError
        {
            get
            {
                lock (this)
                {
                    return _lastError;
                }
            }
        }

        public async void Perform(Func<Task> asyncDelegate)
        {
            try
            {
                lock (this)
                {
                    _isBusy.Value = true;
                    _lastError.Value = null;
                }
                await asyncDelegate();
                lock (this)
                {
                    _isBusy.Value = false;
                }
            }
            catch (Exception x)
            {
                lock (this)
                {
                    _isBusy.Value = false;
                    _lastError.Value = x.Message;
                }
            }
        }
    }
}
