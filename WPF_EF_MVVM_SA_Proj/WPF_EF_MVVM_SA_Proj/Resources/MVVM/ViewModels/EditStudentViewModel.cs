using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_EF_MVVM_SA_Proj.Resources.MVVM.Models;

namespace WPF_EF_MVVM_SA_Proj.Resources.MVVM.ViewModels
{
    public class EditStudentViewModel:DataManageVM
    {
        #region EDIT COMMANDS
        private RelayCommand editStudent;
        public RelayCommand EditStudent
        {
            get
            {
                return editStudent ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран студент";
                    string noGroupStr = "Не выбрана новая группа";
                    if (SelectedStudent != null)
                    {
                        if (StudentGroup != null)
                        {
                            resultStr = DataWorker.EditStudent(SelectedStudent, StudentFIO, StudentGroup);

                            UpdateInfoView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else ShowMessageToUser(noGroupStr);
                    }
                    else ShowMessageToUser(resultStr);

                }
                );
            }
        }
        #endregion
    }
}
