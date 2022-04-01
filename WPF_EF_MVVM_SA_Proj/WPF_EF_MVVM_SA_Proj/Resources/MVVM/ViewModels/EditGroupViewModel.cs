using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_EF_MVVM_SA_Proj.Resources.MVVM.Models;

namespace WPF_EF_MVVM_SA_Proj.Resources.MVVM.ViewModels
{
    public class EditGroupViewModel:DataManageVM
    {
        #region COMMANDS TO EDIT
        private RelayCommand editGroup;
        public RelayCommand EditGroup
        {
            get
            {
                return editGroup ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбрана группа";
                    if (SelectedGroup != null)
                    {
                        resultStr = DataWorker.EditGroup(SelectedGroup, GroupName, Course);

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
