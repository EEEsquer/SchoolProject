using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchoolProject.ViewModel
{
    internal class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;


        public RelayCommand(Action execute, Func<bool> CanExecute = null)
        {
            _execute = execute;
            _canExecute= CanExecute;
        }


        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke();
        }
    }
}
