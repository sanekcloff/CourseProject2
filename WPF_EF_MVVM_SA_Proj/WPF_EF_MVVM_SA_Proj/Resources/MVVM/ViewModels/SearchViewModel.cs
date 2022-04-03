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
    public class SearchViewModel:DataManageVM
    {
        private RelayCommand sortStudentWorkWindowView;
        public RelayCommand SortStudentWorkWindowView
        {
            get
            {
                return sortStudentWorkWindowView ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if (StudentInf == null)
                    {
                        ShowMessageToUser("Выберите студента!");
                    }
                    else
                    {
                        UpdateGradesWWInfoSortedByStudent();
                        wnd.Close();
                    }
                }
                );
            }
        }
        private RelayCommand sortStudentComboBox;
        public RelayCommand SortStudentComboBox
        {
            get
            {
                return sortStudentComboBox ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if (GroupInf == null)
                    {
                        ShowMessageToUser("Выберите группу!");
                    }
                    else
                    {
                        UpdateStudentComboBoxInfo();
                    }
                }
                );
            }
        }
        private void UpdateStudentComboBoxInfo()
        {
            AllStudentsByGroupId = DataWorker.GetAllStudentsByGroupId(GroupInf.Id);
            SearchStudentWindow.SortStudentComboBox.ItemsSource = null;
            SearchStudentWindow.SortStudentComboBox.Items.Clear();
            SearchStudentWindow.SortStudentComboBox.ItemsSource = AllStudentsByGroupId;
            SearchStudentWindow.SortStudentComboBox.Items.Refresh();
        }
    }
}
