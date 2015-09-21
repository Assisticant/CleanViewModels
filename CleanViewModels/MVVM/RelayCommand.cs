using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CleanViewModels.MVVM
{
    class RelayCommand : ICommand
    {
        private readonly Action _execute;

        private bool _canExecute = true;

        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _execute();
        }

        public void SetCanExecute(bool value)
        {
            if (_canExecute != value)
            {
                _canExecute = value;
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, new EventArgs());
            }
        }
    }
}
