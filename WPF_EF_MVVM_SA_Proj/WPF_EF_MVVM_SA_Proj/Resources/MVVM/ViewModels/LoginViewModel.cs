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
    public class LoginViewModel:DataManageVM
    {
        #region CheckLoginPssword
        private RelayCommand checkAuthUser;
        public RelayCommand CheckAuthUser
        {
            get
            {
                return checkAuthUser ?? new RelayCommand(obj =>
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
                        resultStr = "Неправильный логин или пароль";
                        if (DataWorker.CheckAuthUser(Login, Password))
                        {
                            resultStr = "Успешный вход";
                            ShowMessageToUser(resultStr);
                            OpenWorkWindowMethod();
                            SetNullValuesToProperties();
                            wnd.Close();
                        }
                        else
                        {
                            ShowMessageToUser(resultStr);
                        }
                        SetNullValuesToProperties();
                    }
                }
                );
            }
        }
        #endregion
        #region Commands To Open Window
        private RelayCommand openRegisterWindow;
        public RelayCommand OpenRegisterWindow
        {
            get
            {
                return openRegisterWindow ?? new RelayCommand(obj =>
                {
                    OpenRegisterWindowMethod();
                }
                    );
            }
        }
        private RelayCommand openWorkWindow;
        public RelayCommand OpenWorkWindow
        {
            get
            {
                return openWorkWindow ?? new RelayCommand(obj =>
                {
                    OpenWorkWindowMethod();
                    SetNullValuesToProperties();
                }
                    );
            }
        }
        #endregion
        #region Methods To Open Window
        private void OpenRegisterWindowMethod()
        {
            RegistrationWindow newRegisterWindow = new RegistrationWindow();
            SetCenterPositionAndOpen(newRegisterWindow);
        }
        protected void OpenWorkWindowMethod()
        {
            WorkWindow newWorkWindow = new WorkWindow();
            SetCenterPositionAndOpen(newWorkWindow);
        }
        #endregion
    }
}
