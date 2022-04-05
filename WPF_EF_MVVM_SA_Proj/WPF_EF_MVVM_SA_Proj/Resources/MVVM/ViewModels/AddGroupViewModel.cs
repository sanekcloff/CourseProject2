using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_EF_MVVM_SA_Proj.Resources.MVVM.Models;

namespace WPF_EF_MVVM_SA_Proj.Resources.MVVM.ViewModels
{
    public class AddGroupViewModel:DataManageVM
    {
        #region COMMANDS TO ADD
        private RelayCommand addNewGroup;
        public RelayCommand AddNewGroup
        {
            get
            {
                return addNewGroup ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (GroupName == null || GroupName.Replace(" ", "").Length == 0 ||
                    GroupName == null || GroupName.Replace(" ", "").Length == 0)
                    {
                        if (GroupName == null || GroupName.Replace(" ", "").Length == 0)
                        {
                            ShowMessageToUser("Некорректное название");
                        }
                        if (Course == 0)
                        {
                            ShowMessageToUser("Укажите курс");
                        }
                    }
                    else
                    {
                        resultStr = DataWorker.CreateGroup(GroupName, Course);
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
