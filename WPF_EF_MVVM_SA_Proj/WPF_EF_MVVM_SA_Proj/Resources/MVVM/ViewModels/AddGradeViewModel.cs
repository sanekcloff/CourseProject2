using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_EF_MVVM_SA_Proj.Resources.MVVM.Models;

namespace WPF_EF_MVVM_SA_Proj.Resources.MVVM.ViewModels
{
    public class AddGradeViewModel:DataManageVM
    {
        #region COMMANDS TO ADD
        private RelayCommand addNewGrade;
        public RelayCommand AddNewGrade
        {
            get
            {
                return addNewGrade ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (GradeStudent == null || GradeDiscipline == null || GradeValue == 0 || Date == null)
                    {
                        if (GradeStudent == null)
                        {
                            ShowMessageToUser("Некорректный студент");
                        }
                        if (GradeDiscipline == null)
                        {
                            ShowMessageToUser("Некорректное название дисциплины");
                        }
                        if (GradeValue == 0)
                        {
                            ShowMessageToUser("Некорректная оценка");
                        }
                        if (Date == null)
                        {
                            ShowMessageToUser("Некорректная дата");
                        }
                    }
                    else
                    {
                        resultStr = DataWorker.CreateGrade(GradeValue, Date, GradeDiscipline, GradeStudent);
                        UpdateWWInfoView();
                        UpdateGradesEDWInfo();
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
