using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_EF_MVVM_SA_Proj.Resources.MVVM.Models;

namespace WPF_EF_MVVM_SA_Proj.Resources.MVVM.ViewModels
{
    public class EditDisciplineViewModel:DataManageVM
    {
        #region COMMANDS TO EDIT
        private RelayCommand editDiscipline;
        public RelayCommand EditDiscipline
        {
            get
            {
                return editDiscipline ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбрана дисциплина";
                    if (SelectedDiscipline != null)
                    {
                        resultStr = DataWorker.EditDiscipline(SelectedDiscipline, DisciplineName);

                        UpdateInfoView();
                        SetNullValuesToProperties();
                        ShowMessageToUser(resultStr);
                        window.Close();
                    }
                    else ShowMessageToUser(resultStr);
                }
                );
            }
        }
        #endregion
    }
}
