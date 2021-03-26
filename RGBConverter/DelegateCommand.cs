using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RGBConverter
{
    class DelegateCommand : ICommand
    {
        private readonly Action<object> _executeAction;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }
    }
}
