using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanViewModels
{
    class MainViewModel
    {
        private readonly IServiceStatus _serviceStatus;

        public MainViewModel(IServiceStatus serviceStatus)
        {
            _serviceStatus = serviceStatus;            
        }

        public bool IsBusy
        {
            get { return _serviceStatus.IsBusy; }
        }

        public bool HasError
        {
            get { return !string.IsNullOrEmpty(_serviceStatus.LastError); }
        }

        public string LastError
        {
            get { return _serviceStatus.LastError; }
        }
    }
}
