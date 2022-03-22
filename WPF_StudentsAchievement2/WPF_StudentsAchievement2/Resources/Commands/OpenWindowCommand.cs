using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_StudentsAchievement2.Resources.Commands
{
    public class OpenWindowCommand :Command
    {
        private readonly Window _window;
        public OpenWindowCommand(Window window, Predicate<object> predicate=null)
        {
            _predicate = predicate;
            _window = window;
        }
        public override void Execute(object? parameter)
        {
           _window.Show();
        }

        public override bool CanExecute(object? paramenter)
        {
            if (_predicate == null)
                return true;

            return _predicate(paramenter);
        }
    }
}
