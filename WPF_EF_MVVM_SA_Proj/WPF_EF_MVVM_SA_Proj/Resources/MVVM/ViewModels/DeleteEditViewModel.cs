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
    public class DeleteEditViewModel:DataManageVM
    {
        
        #region Delete Commands
        private RelayCommand deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    //если группа
                    if (SelectedTabItem.Name == "GroupsTab" && SelectedGroup != null)
                    {
                        resultStr = DataWorker.DeleteGroup(SelectedGroup);
                        UpdateInfoView();
                    }
                    //если студент
                    if (SelectedTabItem.Name == "StudentsTab" && SelectedStudent != null)
                    {
                        resultStr = DataWorker.DeleteStudent(SelectedStudent);
                        UpdateInfoView();
                    }
                    //если дисциплина
                    if (SelectedTabItem.Name == "DisciplinesTab" && SelectedDiscipline != null)
                    {
                        resultStr = DataWorker.DeleteDiscipline(SelectedDiscipline);
                        UpdateInfoView();
                    }
                    //если отдел
                    if (SelectedTabItem.Name == "GradesTab" && SelectedGrade != null)
                    {
                        resultStr = DataWorker.DeleteGrade(SelectedGrade);
                        UpdateInfoView();
                    }
                    //обновление
                    SetNullValuesToProperties();
                    ShowMessageToUser(resultStr);
                }
                    );
            }
        }
        #endregion

        #region COMMANDS TO OPEN WINDOWS
        private RelayCommand openEditItemWnd;
        public RelayCommand OpenEditItemWnd
        {
            get
            {
                return openEditItemWnd ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    //если группа
                    if (SelectedTabItem.Name == "GroupsTab" && SelectedGroup != null)
                    {
                        OpenEditGroupWindowMethod(SelectedGroup);
                    }
                    //если оценка
                    if (SelectedTabItem.Name == "GradesTab" && SelectedGrade != null)
                    {
                        OpenEditGradeWindowMethod(SelectedGrade);
                    }
                    //если дисциплина
                    if (SelectedTabItem.Name == "DisciplinesTab" && SelectedDiscipline != null)
                    {
                        OpenEditDisciplineWindowMethod(SelectedDiscipline);
                    }
                    //если студент
                    if (SelectedTabItem.Name == "StudentsTab" && SelectedStudent != null)
                    {
                        OpenEditStudentWindowMethod(SelectedStudent);
                    }
                }
                    );
            }
        }
        private RelayCommand openAddItemWnd;
        public RelayCommand OpenAddItemWnd
        {
            get
            {
                return openAddItemWnd ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    //если группа
                    if (SelectedTabItem.Name == "GroupsTab")
                    {
                        OpenAddGroupWindowMethod();
                    }
                    //если оценка
                    if (SelectedTabItem.Name == "GradesTab")
                    {
                        OpenAddGradeWindowMethod();
                    }
                    //если дисциплина
                    if (SelectedTabItem.Name == "DisciplinesTab")
                    {
                        OpenAddDisciplineWindowMethod();
                    }
                    //если студент
                    if (SelectedTabItem.Name == "StudentsTab")
                    {
                        OpenAddStudentWindowMethod();
                    }
                }
                    );
            }
        }

        private RelayCommand openSearchGroupWindow;
        public RelayCommand OpenSearchGroupWindow
        {
            get
            {
                return openSearchGroupWindow ?? new RelayCommand(obj =>
                {
                    WindowSelect = 2;
                    OpenSearchGroupWindowMethod();
                }
                    );
            }
        }
        private RelayCommand openGradeSearchGroupWindow;
        public RelayCommand OpenGradeSearchGroupWindow
        {
            get
            {
                return openGradeSearchGroupWindow ?? new RelayCommand(obj =>
                {
                    WindowSelect = 3;
                    OpenSearchGroupWindowMethod();
                }
                    );
            }
        }
        private RelayCommand openSearchDisciplineWindow;
        public RelayCommand OpenSearchDisciplineWindow
        {
            get
            {
                return openSearchDisciplineWindow ?? new RelayCommand(obj =>
                {
                    WindowSelect = 2;
                    OpenSearchDisciplineWindowMethod();
                }
                    );
            }
        }
        private RelayCommand openSearchStudentDisciplineWindow;
        public RelayCommand OpenSearchStudentDisciplineWindow
        {
            get
            {
                return openSearchStudentDisciplineWindow ?? new RelayCommand(obj =>
                {
                    WindowSelect = 2;
                    OpenSearchStudentDisciplineWindowMethod();
                }
                    );
            }
        }
        private RelayCommand openSearchStudentWindow;
        public RelayCommand OpenSearchStudentWindow
        {
            get
            {
                return openSearchStudentWindow ?? new RelayCommand(obj =>
                {
                    WindowSelect = 2;
                    OpenSearchStudentWindowMethod();
                }
                    );
            }
        }

        public RelayCommand ClearSortStudents
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    SetWindowNull();
                    ClearStudents();
                }
                    );
            }
        }
        public RelayCommand ClearSortGrades
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    SetWindowNull();
                    ClearsGrades();
                }
                    );
            }
        }

        #endregion
        #region METHODS TO OPEN WINDOW
        //методы открытия окон
        private void OpenEditGroupWindowMethod(Group group)
        {
            EditGroupWindow newEditGroupWindow = new EditGroupWindow(group);
            SetCenterPositionAndOpen(newEditGroupWindow);
        }
        private void OpenEditGradeWindowMethod(Grade grade)
        {
            EditGradeWindow newEditGradeWindow = new EditGradeWindow(grade);
            SetCenterPositionAndOpen(newEditGradeWindow);
        }
        private void OpenEditStudentWindowMethod(Student student)
        {
            EditStudentWindow newEditStudentWindow = new EditStudentWindow(student);
            SetCenterPositionAndOpen(newEditStudentWindow);
        }
        private void OpenEditDisciplineWindowMethod(Discipline discipline)
        {
            EditDisciplineWindow newEditDisciplineWindow = new EditDisciplineWindow(discipline);
            SetCenterPositionAndOpen(newEditDisciplineWindow);
        }
        //окно добавления
        private void OpenAddGradeWindowMethod()
        {
            AddGradeWindow newAddGradeWindow = new AddGradeWindow();
            SetCenterPositionAndOpen(newAddGradeWindow);
        }
        private void OpenAddDisciplineWindowMethod()
        {
            AddDisciplineWindow newAddDisciplineWindow = new AddDisciplineWindow();
            SetCenterPositionAndOpen(newAddDisciplineWindow);
        }
        private void OpenAddStudentWindowMethod()
        {
            AddStudentWindow newAddStudentWindow = new AddStudentWindow();
            SetCenterPositionAndOpen(newAddStudentWindow);
        }
        private void OpenAddGroupWindowMethod()
        {
            AddGroupWindow newAddGroupWindow = new AddGroupWindow();
            SetCenterPositionAndOpen(newAddGroupWindow);
        }
        private void OpenSearchGroupWindowMethod()
        {
            SearchGroupWindow newSearchGroupWindow = new SearchGroupWindow();
            SetCenterPositionAndOpen(newSearchGroupWindow);
        }
        private void OpenSearchDisciplineWindowMethod()
        {
            SearchDisciplineWindow newSearchDisciplineWindow = new SearchDisciplineWindow();
            SetCenterPositionAndOpen(newSearchDisciplineWindow);
        }
        private void OpenSearchStudentWindowMethod()
        {
            SearchStudentWindow newSearchStudentWindow = new SearchStudentWindow();
            SetCenterPositionAndOpen(newSearchStudentWindow);
        }
        private void OpenSearchStudentDisciplineWindowMethod()
        {
            SearchStudentDisciplineWindow newSearchStudentDisciplineWindow = new SearchStudentDisciplineWindow();
            SetCenterPositionAndOpen(newSearchStudentDisciplineWindow);
        }
        #endregion

        #region clears
        private void ClearStudents()
        {
            DeleteEditWindow.AllStudentInfoListView.ItemsSource = null;
            DeleteEditWindow.AllStudentInfoListView.Items.Clear();
            DeleteEditWindow.AllStudentInfoListView.ItemsSource = DataWorker.GetAllStudents();
            DeleteEditWindow.AllStudentInfoListView.Items.Refresh();
        }
        private void ClearsGrades()
        {
            DeleteEditWindow.AllGradeInfoListView.ItemsSource = null;
            DeleteEditWindow.AllGradeInfoListView.Items.Clear();
            DeleteEditWindow.AllGradeInfoListView.ItemsSource = DataWorker.GetAllGrades();
            DeleteEditWindow.AllGradeInfoListView.Items.Refresh();
        }
        #endregion
    }
}
