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
    public class AddDisciplineViewModel:DataManageVM
    {
        #region COMMANDS TO ADD
        private RelayCommand addNewDiscipline;
        public RelayCommand AddNewDiscipline
        {
            get
            {
                return addNewDiscipline ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (DisciplineName == null || DisciplineName.Replace(" ", "").Length == 0)
                    {
                        DataManageVM.ShowMessageToUser("Некорректное название");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateDiscipline(DisciplineName);
                        UpdateInfoView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }
        #endregion
    }
}
