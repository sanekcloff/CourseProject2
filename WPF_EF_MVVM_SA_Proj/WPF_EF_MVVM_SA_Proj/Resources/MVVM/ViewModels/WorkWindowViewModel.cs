using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_EF_MVVM_SA_Proj.Resources.MVVM.Models;
using WPF_EF_MVVM_SA_Proj.Resources.MVVM.Views;

namespace WPF_EF_MVVM_SA_Proj.Resources.MVVM.ViewModels
{
    public class WorkWindowViewModel:DataManageVM
    {
        #region RefreshComands
        private RelayCommand standartWorkWindowView;
        public RelayCommand StandartWorkWindowView
        {
            get
            {
                return standartWorkWindowView ?? new RelayCommand(obj =>
                {
                    UpdateGradesWWInfo();
                    StudentInf = null;
                    GroupInf = null;
                }
                );
            }
        }
        
        #endregion

        #region Commands To Open Window
        private RelayCommand openDeleteEditWindow;
        public RelayCommand OpenDeleteEditWindow
        {
            get
            {
                return openDeleteEditWindow ?? new RelayCommand(obj =>
                {
                    OpenDeleteEditWindowMethod();
                    UpdateInfoView();
                }
                    );
            }
        }
        private RelayCommand openSearchStudentWindow;
        public RelayCommand OpenSearchStudentWindow
        {
            get
            {
                return openSearchStudentWindow ?? new RelayCommand(obj =>
                {
                    OpenSearchStudentWindowMethod();
                    //UpdateInfoView();
                }
                    );
            }
        }
        #endregion
        #region Methods To Open Window
        private void OpenDeleteEditWindowMethod()
        {
            DeleteEditWindow newDeleteEditWindow = new DeleteEditWindow();
            SetCenterPositionAndOpen(newDeleteEditWindow);
        }
        private void OpenSearchStudentWindowMethod()
        {
            SearchStudentWindow newSearchStudentWindow = new SearchStudentWindow();
            SetCenterPositionAndOpen(newSearchStudentWindow);
        }
        #endregion
    }
}
