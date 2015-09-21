using Assisticant;
using CleanViewModels.PodcastEpisode.Models;
using CleanViewModels.PodcastEpisode.Services;
using CleanViewModels.PodcastEpisode.Wizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CleanViewModels
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Func<Upload, WizardViewModel> _makeWizardViewModel;
        private readonly IUploadService _uploadService;

        public MainWindow()
        {
            InitializeComponent();

            ContainerConfig container = new ContainerConfig();
            _makeWizardViewModel = container.Resolve<Func<Upload, WizardViewModel>>();
            _uploadService = container.Resolve<IUploadService>();
        }

        private void LaunchWizard(object sender, RoutedEventArgs e)
        {
            // Create the model and inject it into the view models.
            var upload = new Upload();
            var viewModel = _makeWizardViewModel(upload);
            viewModel.LoadAsync();

            // Show the dialog with the view models.
            var dialog = new WizardDialog();
            dialog.DataContext = ForView.Wrap(viewModel);
            dialog.ShowDialog();
        }
    }
}
