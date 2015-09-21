using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CleanViewModels.PodcastEpisode.Wizard
{
    /// <summary>
    /// Interaction logic for WizardDialog.xaml
    /// </summary>
    public partial class WizardDialog : Window
    {
        public WizardDialog()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((WizardViewModel)DataContext).Closed += ViewModel_Close;
        }

        private void ViewModel_Close(object sender, EventArgs e)
        {
            ((WizardViewModel)DataContext).Closed -= ViewModel_Close;
            Close();
        }
    }
}
