using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_StudentsAchievement2.Resources.Commands;
using WPF_StudentsAchievement2.Resources.MVVM.View;

namespace WPF_StudentsAchievement2.Resources.MVVM.ViewModel
{
    public class LoginViewModel:ViewModel
    {
        #region Fields
        private string _login;
        private string _password;
        #endregion

        public ICommand OpenAddNewCommand => new OpenWindowCommand(new RegistrationWindow());

        #region Properties
        public string Login 
        { 
            get => _login;
            set 
            {
                Set(ref _login,value,nameof(Login));
            } 
        }
        public string Password 
        { 
            get => _password;
            set
            {
                Set(ref _password, value, nameof(Password));
            }
        }
        #endregion


    }
}
