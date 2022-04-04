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
        private RelayCommand sortGroupWorkWindowView;
        public RelayCommand SortGroupWorkWindowView
        {
            get
            {
                return sortGroupWorkWindowView ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if (GroupInf == null)
                    {
                        ShowMessageToUser("Выберите группу!");
                    }
                    else
                    {
                        UpdateGradesWWInfoSortedByGroup();
                        wnd.Close();
                    }
                }
                );
            }
        }
        private RelayCommand sortStudentDisciplineWorkWindowView;
        public RelayCommand SortStudentDisciplineWorkWindowView
        {
            get
            {
                return sortStudentDisciplineWorkWindowView ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if (StudentInf == null)
                    {
                        ShowMessageToUser("Выберите студента!");
                    }
                    if (DisciplineInf == null)
                    {
                        ShowMessageToUser("Выберите студента!");
                    }
                    else
                    {
                        UpdateGradesWWInfoSortedByStudentDiscipline();
                        wnd.Close();
                    }
                }
                );
            }
        }
        private RelayCommand sortDisciplineWorkWindowView;
        public RelayCommand SortDisciplineWorkWindowView
        {
            get
            {
                return sortDisciplineWorkWindowView ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if (DisciplineInf == null)
                    {
                        ShowMessageToUser("Выберите дисциплину!");
                    }
                    else
                    {
                        UpdateGradesWWInfoSortedByDiscipline();
                        wnd.Close();
                    }
                }
                );
            }
        }
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
        private RelayCommand sortStudentDisciplineComboBox;
        public RelayCommand SortStudentDisciplineComboBox
        {
            get
            {
                return sortStudentDisciplineComboBox ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if (GroupInf == null)
                    {
                        ShowMessageToUser("Выберите группу!");
                    }
                    else
                    {
                        UpdateStudentDisciplineComboBoxInfo();
                    }
                }
                );
            }
        }
        private void UpdateGradesWWInfoSortedByGroup()
        {
            AllGradesByGroupId = DataWorker.GetAllGradesByGroupId(GroupInf.Id);
            WorkWindow.AllGradeInfoListView.ItemsSource = null;
            WorkWindow.AllGradeInfoListView.Items.Clear();
            WorkWindow.AllGradeInfoListView.ItemsSource = AllGradesByGroupId;
            WorkWindow.AllGradeInfoListView.Items.Refresh();
        }
        private void UpdateGradesWWInfoSortedByStudentDiscipline()
        {
            AllGradesByStudentDisciplineId = DataWorker.GetAllGradesByStudentDisciplineId(StudentInf.Id,DisciplineInf.Id);
            WorkWindow.AllGradeInfoListView.ItemsSource = null;
            WorkWindow.AllGradeInfoListView.Items.Clear();
            WorkWindow.AllGradeInfoListView.ItemsSource = AllGradesByStudentDisciplineId;
            WorkWindow.AllGradeInfoListView.Items.Refresh();
        }
        private void UpdateGradesWWInfoSortedByDiscipline()
        {
            AllGradesByDisciplineId = DataWorker.GetAllGradesByDisciplineId(DisciplineInf.Id);
            WorkWindow.AllGradeInfoListView.ItemsSource = null;
            WorkWindow.AllGradeInfoListView.Items.Clear();
            WorkWindow.AllGradeInfoListView.ItemsSource = AllGradesByDisciplineId;
            WorkWindow.AllGradeInfoListView.Items.Refresh();
        }
        private void UpdateGradesWWInfoSortedByStudent()
        {
            AllGradesByStudentId = DataWorker.GetAllGradesByStudentId(StudentInf.Id);
            WorkWindow.AllGradeInfoListView.ItemsSource = null;
            WorkWindow.AllGradeInfoListView.Items.Clear();
            WorkWindow.AllGradeInfoListView.ItemsSource = AllGradesByStudentId;
            WorkWindow.AllGradeInfoListView.Items.Refresh();
        }
        private void UpdateStudentComboBoxInfo()
        {
            AllStudentsByGroupId = DataWorker.GetAllStudentsByGroupId(GroupInf.Id);
            SearchStudentWindow.SortStudentComboBox.ItemsSource = null;
            SearchStudentWindow.SortStudentComboBox.Items.Clear();
            SearchStudentWindow.SortStudentComboBox.ItemsSource = AllStudentsByGroupId;
            SearchStudentWindow.SortStudentComboBox.Items.Refresh();
        }
        private void UpdateStudentDisciplineComboBoxInfo()
        {
            AllStudentsByGroupId = DataWorker.GetAllStudentsByGroupId(GroupInf.Id);
            SearchStudentDisciplineWindow.SortStudentComboBox.ItemsSource = null;
            SearchStudentDisciplineWindow.SortStudentComboBox.Items.Clear();
            SearchStudentDisciplineWindow.SortStudentComboBox.ItemsSource = AllStudentsByGroupId;
            SearchStudentDisciplineWindow.SortStudentComboBox.Items.Refresh();
        }
    }
}
