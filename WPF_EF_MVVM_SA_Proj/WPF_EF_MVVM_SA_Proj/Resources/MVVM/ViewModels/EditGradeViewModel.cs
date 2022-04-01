using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_EF_MVVM_SA_Proj.Resources.MVVM.Models;

namespace WPF_EF_MVVM_SA_Proj.Resources.MVVM.ViewModels
{
    public class EditGradeViewModel:DataManageVM
    {
        #region COMMANDS TO EDIT
        private RelayCommand editGrade;
        public RelayCommand EditGrade
        {
            get
            {
                return editGrade ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбрана Оценка";
                    string noStudentStr = "Не выбран студент";
                    string noDisciplineStr = "Не выбран студент";
                    if (SelectedGrade != null)
                    {
                        if (GradeStudent != null)
                        {
                            if (GradeDiscipline != null)
                            {
                                resultStr = DataWorker.EditGrade(SelectedGrade, GradeStudent, GradeDiscipline, GradeValue, Date);

                                UpdateInfoView();
                                SetNullValuesToProperties();
                                ShowMessageToUser(resultStr);
                                window.Close();
                            }
                            else ShowMessageToUser(noDisciplineStr);
                        }
                        else ShowMessageToUser(noStudentStr);
                    }
                    else ShowMessageToUser(resultStr);
                }
                );
            }
        }
        #endregion
    }
}
