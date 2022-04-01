using WPF_EF_MVVM_SA_Proj.Resources.MVVM.Models;
using WPF_EF_MVVM_SA_Proj.Resources.MVVM.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF_EF_MVVM_SA_Proj.Resources.MVVM.ViewModels
{
    public class DataManageVM : INotifyPropertyChanged
    {
        //все Студенты
        private List<Student> allStudents = DataWorker.GetAllStudents();
        public List<Student> AllStudents
        {
            get { return allStudents; }
            set
            {
                allStudents = value;
                NotifyPropertyChanged("AllStudents");
            }
        }
        //все Дисциплины
        private List<Discipline> allDisciplines = DataWorker.GetAllDisciplines();
        public List<Discipline> AllDisciplines
        {
            get { return allDisciplines; }
            set
            {
                allDisciplines = value;
                NotifyPropertyChanged("AllDisciplines");
            }
        }
        //все Группы
        private List<Group> allGroups = DataWorker.GetAllGroups();
        public List<Group> AllGroups
        {
            get { return allGroups; }
            set
            {
                allGroups = value;
                NotifyPropertyChanged("AllGroups");
            }
        }

        //все Оценки
        private List<Grade> allGrades = DataWorker.GetAllGrades();
        public List<Grade> AllGrades
        {
            get { return allGrades; }
            set
            {
                allGrades = value;
                NotifyPropertyChanged("AllGrades");
            }
        }
        //все Оценки по группе
        private List<Grade> allGradesByStudentId;
        public List<Grade> AllGradesByStudentId
        {
            get { return allGradesByStudentId; }
            set
            {
                allGradesByStudentId = value;
                NotifyPropertyChanged("AllGradesByStudent");
            }
        }

        //свойства для групп
        public static string GroupName { get; set; }
        public static int Course { get; set; }
        //свойства для дисциплин
        public static string DisciplineName { get; set; }
        //свойства для пользователя
        public static string Login { get; set; }
        public static string Password { get; set; }
        //свойства для Студента
        public static Student StudentInf { get; set; }
        public static string StudentFIO { get; set; }
        public static Group StudentGroup { get; set; }

        //свойства для Оценки
        public static Student GradeStudent { get; set; }
        public static Discipline GradeDiscipline { get; set; }
        public static int GradeValue { get; set; }
        public static DateTime Date { get; set; }


        //свойства для выделенных элементов
        public TabItem SelectedTabItem { get; set; }
        public static Student SelectedStudent { get; set; }
        public static Group SelectedGroup { get; set; }
        public static Grade SelectedGrade { get; set; }
        public static Discipline SelectedDiscipline { get; set; }



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
                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }
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
                        UpdateWWInfoView();
                        UpdateSortComboBoxItems();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }
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
                        UpdateWWInfoView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }
        private RelayCommand addNewDiscipline;
        public RelayCommand AddNewDiscipline
        {
            get
            {
                return addNewDiscipline ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (DisciplineName == null || DisciplineName.Replace(" ", "").Length == 0)
                    {
                        ShowMessageToUser("Некорректное название");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateDiscipline(DisciplineName);
                        UpdateWWInfoView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }
        private RelayCommand addNewUser;
        public RelayCommand AddNewUser
        {
            get
            {
                return addNewUser ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (Login == null || Login.Replace(" ", "").Length == 0)
                    {
                        ShowMessageToUser("Некорректный логин");
                    }
                    if (Password == null || Password.Replace(" ", "").Length == 0)
                    {
                        ShowMessageToUser("Некорректный пароль");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateUser(Login, Password);

                        ShowMessageToUser(resultStr);
                        wnd.Close();
                        SetNullValuesToProperties();
                    }
                }
                );
            }
        }
        #endregion
        #region CheckLoginPssword
        private RelayCommand checkAuthUser;
        public RelayCommand CheckAuthUser
        {
            get
            {
                return checkAuthUser ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (Login == null || Login.Replace(" ", "").Length == 0)
                    {
                        ShowMessageToUser("Некорректный логин");
                    }
                    if (Password == null || Password.Replace(" ", "").Length == 0)
                    {
                        ShowMessageToUser("Некорректный пароль");
                    }
                    else
                    {
                        resultStr = "Неправильный логин или пароль";
                        if (DataWorker.CheckAuthUser(Login, Password))
                        {
                            resultStr = "Успешный вход";
                            ShowMessageToUser(resultStr);
                            OpenWorkWindowMethod();
                            //UpdateInfoView();
                            SetNullValuesToProperties();
                            wnd.Close();
                        }
                        else
                        {
                            ShowMessageToUser(resultStr);
                        }
                        SetNullValuesToProperties();
                    }
                }
                );
            }
        }
        #endregion
        #region RefreshComands
        private RelayCommand standartWorkWindowView;
        public RelayCommand StandartWorkWindowView
        {
            get
            {
                return standartWorkWindowView ?? new RelayCommand(obj =>
                {
                    UpdateGradesWWInfo();
                    WorkWindow.WorkWindowSortComboBox.SelectedValue = null;
                    StudentInf = null;
                }
                );
            }
        }
        private RelayCommand refreshWorkWindowView;
        public RelayCommand RefreshWorkWindowView
        {
            get
            {
                return refreshWorkWindowView ?? new RelayCommand(obj =>
                {
                    if (StudentInf == null)
                    {
                        ShowMessageToUser("Выберите пункт!");
                    }
                    else
                    {
                        UpdateGradesWWInfoSortedByStudent();
                    }
                }
                );
            }
        }
        #endregion

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
        private RelayCommand editDiscipline;
        public RelayCommand EditDiscipline
        {
            get
            {
                return editDiscipline ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбрана дисциплина";
                    if (SelectedDiscipline != null)
                    {
                        resultStr = DataWorker.EditDiscipline(SelectedDiscipline, DisciplineName);

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
        private RelayCommand openDeleteEditWindow;
        public RelayCommand OpenDeleteEditWindow
        {
            get
            {
                return openDeleteEditWindow ?? new RelayCommand(obj =>
                {
                    OpenDeleteEditWindowMethod();
                    UpdateInfoView();
                }
                    );
            }
        }
        private RelayCommand openWorkWindow;
        public RelayCommand OpenWorkWindow
        {
            get
            {
                return openWorkWindow ?? new RelayCommand(obj =>
                {
                    OpenWorkWindowMethod();
                    SetNullValuesToProperties();
                }
                    );
            }
        }

        private RelayCommand openRegisterWindow;
        public RelayCommand OpenRegisterWindow
        {
            get
            {
                return openRegisterWindow ?? new RelayCommand(obj =>
                    {
                        OpenRegisterWindowMethod();
                    }
                    );
            }
        }
        private RelayCommand openLoginWindow;
        public RelayCommand OpenLoginWindow
        {
            get
            {
                return openLoginWindow ?? new RelayCommand(obj =>
                {
                    OpenLoginWindowMethod();
                }
                    );
            }
        }
        private RelayCommand openAddGradeWindow;
        public RelayCommand OpenAddGradeWindow
        {
            get
            {
                return openAddGradeWindow ?? new RelayCommand(obj =>
                {
                    OpenAddGradeWindowMethod();
                }
                );
            }
        }
        private RelayCommand openAddDisciplineWindow;
        public RelayCommand OpenAddDisciplineWindow
        {
            get
            {
                return openAddDisciplineWindow ?? new RelayCommand(obj =>
                {
                    OpenAddDisciplineWindowMethod();
                }
                );
            }
        }
        private RelayCommand openAddStudentWindow;
        public RelayCommand OpenAddStudentWindow
        {
            get
            {
                return openAddStudentWindow ?? new RelayCommand(obj =>
                {
                    OpenAddStudentWindowMethod();
                }
                );
            }
        }
        private RelayCommand openAddGroupWindow;
        public RelayCommand OpenAddGroupWindow
        {
            get
            {
                return openAddGroupWindow ?? new RelayCommand(obj =>
                {
                    OpenAddGroupWindowMethod();
                }
                );
            }
        }
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
        private void OpenDeleteEditWindowMethod()
        {
            DeleteEditWindow newDeleteEditWindow = new DeleteEditWindow();
            SetCenterPositionAndOpen(newDeleteEditWindow);
        }
        private void OpenWorkWindowMethod()
        {
            WorkWindow newWorkWindow = new WorkWindow();
            SetCenterPositionAndOpen(newWorkWindow);
        }
        private void OpenLoginWindowMethod()
        {
            LoginWindow newLoginWindow = new LoginWindow();
            SetCenterPositionAndOpen(newLoginWindow);
        }
        private void OpenRegisterWindowMethod()
        {
            RegistrationWindow newRegisterWindow = new RegistrationWindow();
            SetCenterPositionAndOpen(newRegisterWindow);
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
        private void SetCenterPositionAndOpen(Window window)
        {
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }
        #endregion

        #region UPDATE VIEWS
        private void SetNullValuesToProperties()
        {
            //для Студента
            StudentFIO = null;
            StudentGroup = null;
            //для Оценки
            GradeStudent = null;
            GradeDiscipline = null;
            GradeValue = 0;
            Date = DateTime.Today;
            //для Группы
            GroupName = null;
            Course = 0;
            //для Пользоваетля
            Login = null;
            Password = null;
        }
        private void UpdateInfoView()
        {
            UpdateWWInfoView();
            UpdateDisciplinesInfo();
            UpdateGroupsInfo();
            UpdateStudentsInfo();
            UpdateGradesEDWInfo();
        }
        private void UpdateWWInfoView()
        {
            UpdateGradesWWInfo();
        }
        private void UpdateGradesWWInfo()
        {
            AllGrades = DataWorker.GetAllGrades();
            WorkWindow.AllGradeInfoListView.ItemsSource = null;
            WorkWindow.AllGradeInfoListView.Items.Clear();
            WorkWindow.AllGradeInfoListView.ItemsSource = AllGrades;
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
        private void UpdateGradesEDWInfo()
        {
            AllGrades = DataWorker.GetAllGrades();
            DeleteEditWindow.AllGradeInfoListView.ItemsSource = null;
            DeleteEditWindow.AllGradeInfoListView.Items.Clear();
            DeleteEditWindow.AllGradeInfoListView.ItemsSource = AllGrades;
            DeleteEditWindow.AllGradeInfoListView.Items.Refresh();
        }
        private void UpdateGroupsInfo()
        {
            AllGroups = DataWorker.GetAllGroups();
            DeleteEditWindow.AllGroupInfoListView.ItemsSource = null;
            DeleteEditWindow.AllGroupInfoListView.Items.Clear();
            DeleteEditWindow.AllGroupInfoListView.ItemsSource = AllGroups;
            DeleteEditWindow.AllGroupInfoListView.Items.Refresh();
        }
        private void UpdateStudentsInfo()
        {
            AllStudents = DataWorker.GetAllStudents();
            DeleteEditWindow.AllStudentInfoListView.ItemsSource = null;
            DeleteEditWindow.AllStudentInfoListView.Items.Clear();
            DeleteEditWindow.AllStudentInfoListView.ItemsSource = AllStudents;
            DeleteEditWindow.AllStudentInfoListView.Items.Refresh();
        }
        private void UpdateSortComboBoxItems()
        {
            AllStudents = DataWorker.GetAllStudents();
            WorkWindow.WorkWindowSortComboBox.ItemsSource = null;
            WorkWindow.WorkWindowSortComboBox.Items.Clear();
            WorkWindow.WorkWindowSortComboBox.ItemsSource = AllStudents;
            WorkWindow.WorkWindowSortComboBox.Items.Refresh();
        }
        private void UpdateDisciplinesInfo()
        {
            AllDisciplines = DataWorker.GetAllDisciplines();
            DeleteEditWindow.AllDisciplineInfoListView.ItemsSource = null;
            DeleteEditWindow.AllDisciplineInfoListView.Items.Clear();
            DeleteEditWindow.AllDisciplineInfoListView.ItemsSource = AllDisciplines;
            DeleteEditWindow.AllDisciplineInfoListView.Items.Refresh();
        }
        #endregion

        private void ShowMessageToUser(string message)
        {
            MessageBox.Show(message, "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
