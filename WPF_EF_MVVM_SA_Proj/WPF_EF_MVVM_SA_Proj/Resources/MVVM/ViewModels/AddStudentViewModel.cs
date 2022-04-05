using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_EF_MVVM_SA_Proj.Resources.MVVM.Models;

namespace WPF_EF_MVVM_SA_Proj.Resources.MVVM.ViewModels
{
    public class AddStudentViewModel:DataManageVM
    {
        #region COMMANDS TO ADD
        private RelayCommand addNewStudent;
        public RelayCommand AddNewStudent
        {
            get
            {
                return addNewStudent ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (StudentGroup == null || StudentFIO == null || StudentFIO.Replace(" ", "").Length == 0)
                    {
                        if (StudentFIO == null || StudentFIO.Replace(" ", "").Length == 0)
                        {
                            ShowMessageToUser("Некорректное Ф.И.О.");
                        }
                        if (StudentGroup == null)
                        {
                            ShowMessageToUser("Укажите группу");
                        }
                    }
                    else
                    {
                        resultStr = DataWorker.CreateStudent(StudentFIO, StudentGroup);
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
