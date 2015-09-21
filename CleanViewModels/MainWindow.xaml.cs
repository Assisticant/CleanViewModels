using Assisticant;
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
        private readonly ServiceManager _serviceManager;

        public MainWindow()
        {
            InitializeComponent();

            ContainerConfig container = new ContainerConfig();
            _serviceManager = container.Resolve<ServiceManager>();
            DataContext = ForView.Wrap(container.Resolve<MainViewModel>());
        }

        private void LaunchWizard(object sender, RoutedEventArgs e)
        {
            _serviceManager.RunWizard();
        }
    }
}
