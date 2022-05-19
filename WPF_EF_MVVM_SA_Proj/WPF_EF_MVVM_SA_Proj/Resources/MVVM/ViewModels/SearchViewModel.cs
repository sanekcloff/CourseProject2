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
                        switch (WindowSelect)
                        {
                            case 1:
                                UpdateGradesWWInfoSortedByGroup();
                                break;
                            case 2:
                                UpdateStudentsEDInfoSortedByGroup();
                                break;
                            case 3:
                                UpdateGradesEDInfoSortedByGroup();
                                break;
                        }
                        SetWindowNull();
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
                        switch (WindowSelect)
                        {
                            case 1:
                                UpdateGradesWWInfoSortedByStudentDiscipline();
                                break;
                            case 2:
                                UpdateGradesEDInfoSortedByStudentDiscipline();
                                break;
                        }
                        SetWindowNull();
                        
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
                        switch (WindowSelect)
                        {
                            case 1:
                                UpdateGradesWWInfoSortedByDiscipline();
                                break;
                            case 2:
                                UpdateGradesEDInfoSortedByDiscipline();
                                break;
                        }
                        SetWindowNull();
                        
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
                        switch (WindowSelect)
                        {
                            case 1:
                                UpdateGradesWWInfoSortedByStudent();
                                break;
                            case 2:
                                UpdateGradesEDInfoSortedByStudent();
                                break;
                        }
                        SetWindowNull();
                        
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
        #region WorkWindowSort
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
        //НЕ ТРОГАТЬ
        private void UpdateStudentComboBoxInfo()
        {
            AllStudentsByGroupId = DataWorker.GetAllStudentsByGroupId(GroupInf.Id);
            SearchStudentWindow.SortStudentComboBox.ItemsSource = null;
            SearchStudentWindow.SortStudentComboBox.Items.Clear();
            SearchStudentWindow.SortStudentComboBox.ItemsSource = AllStudentsByGroupId;
            SearchStudentWindow.SortStudentComboBox.Items.Refresh();
        }
        //НЕ ТРОГАТЬ
        private void UpdateStudentDisciplineComboBoxInfo()
        {
            AllStudentsByGroupId = DataWorker.GetAllStudentsByGroupId(GroupInf.Id);
            SearchStudentDisciplineWindow.SortStudentComboBox.ItemsSource = null;
            SearchStudentDisciplineWindow.SortStudentComboBox.Items.Clear();
            SearchStudentDisciplineWindow.SortStudentComboBox.ItemsSource = AllStudentsByGroupId;
            SearchStudentDisciplineWindow.SortStudentComboBox.Items.Refresh();
        }
        #endregion
        #region sorts
        private void UpdateStudentsEDInfoSortedByGroup()
        {
            DeleteEditWindow.AllStudentInfoListView.ItemsSource = null;
            DeleteEditWindow.AllStudentInfoListView.Items.Clear();
            DeleteEditWindow.AllStudentInfoListView.ItemsSource = DataWorker.GetAllStudentsByGroupId(GroupInf.Id);
            DeleteEditWindow.AllStudentInfoListView.Items.Refresh();
        }
        private void UpdateGradesEDInfoSortedByGroup()
        {
            DeleteEditWindow.AllGradeInfoListView.ItemsSource = null;
            DeleteEditWindow.AllGradeInfoListView.Items.Clear();
            DeleteEditWindow.AllGradeInfoListView.ItemsSource = DataWorker.GetAllGradesByGroupId(GroupInf.Id);
            DeleteEditWindow.AllGradeInfoListView.Items.Refresh();
        }
        private void UpdateGradesEDInfoSortedByStudentDiscipline()
        {
            DeleteEditWindow.AllGradeInfoListView.ItemsSource = null;
            DeleteEditWindow.AllGradeInfoListView.Items.Clear();
            DeleteEditWindow.AllGradeInfoListView.ItemsSource = DataWorker.GetAllGradesByStudentDisciplineId(StudentInf.Id, DisciplineInf.Id);
            DeleteEditWindow.AllGradeInfoListView.Items.Refresh();
        }
        private void UpdateGradesEDInfoSortedByDiscipline()
        {
            DeleteEditWindow.AllGradeInfoListView.ItemsSource = null;
            DeleteEditWindow.AllGradeInfoListView.Items.Clear();
            DeleteEditWindow.AllGradeInfoListView.ItemsSource = DataWorker.GetAllGradesByDisciplineId(DisciplineInf.Id);
            DeleteEditWindow.AllGradeInfoListView.Items.Refresh();
        }
        private void UpdateGradesEDInfoSortedByStudent()
        {
            DeleteEditWindow.AllGradeInfoListView.ItemsSource = null;
            DeleteEditWindow.AllGradeInfoListView.Items.Clear();
            DeleteEditWindow.AllGradeInfoListView.ItemsSource = DataWorker.GetAllGradesByStudentId(StudentInf.Id);
            DeleteEditWindow.AllGradeInfoListView.Items.Refresh();
        }
        #endregion
    }
}
