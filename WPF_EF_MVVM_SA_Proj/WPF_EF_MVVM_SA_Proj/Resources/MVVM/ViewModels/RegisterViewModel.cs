using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_EF_MVVM_SA_Proj.Resources.MVVM.Models;
using WPF_EF_MVVM_SA_Proj.Resources.MVVM.Views;

namespace WPF_EF_MVVM_SA_Proj.Resources.MVVM.ViewModels
{
    public class RegisterViewModel:DataManageVM
    {
        #region COMMANDS TO ADD
        private RelayCommand addNewUser;
        public RelayCommand AddNewUser
        {
            get
            {
                return addNewUser ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (Login == null || Login.Replace(" ", "").Length == 0)
                    {
                        ShowMessageToUser("Некорректный логин");
                    }
                    if (Password == null || Password.Replace(" ", "").Length == 0)
                    {
                        ShowMessageToUser("Некорректный пароль");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateUser(Login, Password);

                        ShowMessageToUser(resultStr);
                        wnd.Close();
                        SetNullValuesToProperties();
                    }
                }
                );
            }
        }
        #endregion
        #region Commands To Open Window
        private RelayCommand openLoginWindow;
        public RelayCommand OpenLoginWindow
        {
            get
            {
                return openLoginWindow ?? new RelayCommand(obj =>
                {
                    OpenLoginWindowMethod();
                }
                    );
            }
        }
        #endregion
        #region Methods To Open Window
        private void OpenLoginWindowMethod()
        {
            LoginWindow newLoginWindow = new LoginWindow();
            SetCenterPositionAndOpen(newLoginWindow);
        }
        #endregion
    }
}
