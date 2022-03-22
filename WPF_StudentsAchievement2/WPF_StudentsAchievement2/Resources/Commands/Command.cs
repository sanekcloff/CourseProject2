using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_StudentsAchievement2.Resources.Commands
{
    public abstract class Command:ICommand
    {
        protected Action<object> _command;
        protected Predicate<object> _predicate;

        public event EventHandler? CanExecuteChanged;
        public virtual bool CanExecute(object paramenter)
        {
            return _predicate(paramenter);
        }
        public virtual void Execute(object parameter)
        {
            _command(parameter);
        }
        public virtual void OnCanExecuteChanged(object sender,EventArgs e)
        {
            CanExecuteChanged?.Invoke(sender, e);
        }
    }
}
