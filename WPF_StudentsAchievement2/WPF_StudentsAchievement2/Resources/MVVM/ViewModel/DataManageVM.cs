using WPF_StudentsAchievement2.Resources.MVVM.Models;
using WPF_StudentsAchievement2.Resources.MVVM.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF_StudentsAchievement2.Resources.MVVM.ViewModel
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
                NotifyPropertyChanged("AllGroupss");
            }
        }

        //все Оценки
        private List<Grade> allGrades = DataWorker.GetAllGrades();
        public List<Grade> AllGrades
        {
            get { return allGrades; }
            private set
            {
                allGrades = value;
                NotifyPropertyChanged("AllGrades");
            }
        }

        //свойства для отдела
        public static string GroupName { get; set; }
        public static int Course { get; set; }
        //свойства для отдела
        public static string DisciplineName { get; set; }
        //свойства для пользователя
        public static string Login { get; set; }
        public static string Password { get; set; }
        //свойства для Студента
        public static string StudentFirstName { get; set; }
        public static string StudentLasttName { get; set; }
        public static string StudentMiddleName { get; set; }
        public static Group StudentGroup { get; set; }

        //свойства для Оценки
        public static Student GradeStudent { get; set; }
        public static Discipline GradeDiscipline { get; set; }
        public static int GradeValue { get; set; }
        public static DateTime Date { get; set; }

        //свойства для выделенных элементов
        //public TabItem SelectedTabItem { get; set; }
        //public static User SelectedUser { get; set; }
        //public static Position SelectedPosition { get; set; }
        //public static Department SelectedDepartment { get; set; }


        #region COMMANDS TO ADD
        //private RelayCommand addNewDepartment;
        //public RelayCommand AddNewDepartment
        //{
        //    get
        //    {
        //        return addNewDepartment ?? new RelayCommand(obj =>
        //        {
        //            Window wnd = obj as Window;
        //            string resultStr = "";
        //            if (DepartmentName == null || DepartmentName.Replace(" ", "").Length == 0)
        //            {
        //                SetRedBlockControll(wnd, "NameBlock");
        //            }
        //            else
        //            {
        //                resultStr = DataWorker.CreateDepartment(DepartmentName);
        //                UpdateAllDataView();

        //                ShowMessageToUser(resultStr);
        //                SetNullValuesToProperties();
        //                wnd.Close();
        //            }
        //        }
        //        );
        //    }
        //}
        //private RelayCommand addNewPosition;
        //public RelayCommand AddNewPosition
        //{
        //    get
        //    {
        //        return addNewPosition ?? new RelayCommand(obj =>
        //        {
        //            Window wnd = obj as Window;
        //            string resultStr = "";
        //            if(PositionName == null || PositionName.Replace(" ", "").Length == 0)
        //            {
        //                SetRedBlockControll(wnd, "NameBlock");
        //            }
        //            if (PositionSalary == 0)
        //            {
        //                SetRedBlockControll(wnd, "SalaryBlock");
        //            }
        //            if (PositionMaxNumber == 0)
        //            {
        //                SetRedBlockControll(wnd, "MaxNumberBlock");
        //            }
        //            if (PositionDepartment == null)
        //            {
        //                MessageBox.Show("Укажите отдел");
        //            }
        //            else
        //            {
        //                resultStr = DataWorker.CreatePosition(PositionName, PositionSalary, PositionMaxNumber, PositionDepartment);
        //                UpdateAllDataView();

        //                ShowMessageToUser(resultStr);
        //                SetNullValuesToProperties();
        //                wnd.Close();
        //            }
        //        }
        //        );
        //    }
        //}
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
                        //UpdateInfoView();

                        ShowMessageToUser(resultStr);
                        wnd.Close();
                        //SetNullValuesToProperties();
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
                            wnd.Close();
                        }
                        else
                        {
                            ShowMessageToUser(resultStr);
                        }
                        //SetNullValuesToProperties();
                    }
                }
                );
            }
        }
        #endregion
        //private RelayCommand deleteItem;
        //public RelayCommand DeleteItem
        //{
        //    get
        //    {
        //        return deleteItem ?? new RelayCommand(obj =>
        //        {
        //            string resultStr = "Ничего не выбрано";
        //            //если сотрудник
        //            if(SelectedTabItem.Name == "UsersTab" && SelectedUser != null)
        //            {
        //                resultStr = DataWorker.DeleteUser(SelectedUser);
        //                UpdateAllDataView();
        //            }
        //            //если позиция
        //            if (SelectedTabItem.Name == "PositionsTab" && SelectedPosition != null)
        //            {
        //                resultStr = DataWorker.DeletePosition(SelectedPosition);
        //                UpdateAllDataView();
        //            }
        //            //если отдел
        //            if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
        //            {
        //                resultStr = DataWorker.DeleteDepartment(SelectedDepartment);
        //                UpdateAllDataView();
        //            }
        //            //обновление
        //            SetNullValuesToProperties();
        //            ShowMessageToUser(resultStr);
        //        }
        //            );
        //    }
        //}

        //#region EDIT COMMANDS
        //private RelayCommand editUser;
        //public RelayCommand EditUser
        //{
        //    get
        //    {
        //        return editUser ?? new RelayCommand(obj =>
        //        {
        //            Window window = obj as Window;
        //            string resultStr = "Не выбран сотрудник";
        //            string noPositionStr = "Не выбрана новая должность";
        //            if(SelectedUser != null)
        //            {
        //                if(UserPosition != null)
        //                {
        //                    resultStr = DataWorker.EditUser(SelectedUser, UserName, UserSurName, UserPhone, UserPosition);

        //                    UpdateAllDataView();
        //                    SetNullValuesToProperties();
        //                    ShowMessageToUser(resultStr);
        //                    window.Close();
        //                }
        //                else ShowMessageToUser(noPositionStr);
        //            }
        //            else ShowMessageToUser(resultStr);

        //        }
        //        );
        //    }
        //}
        //private RelayCommand editPosition;
        //public RelayCommand EditPosition
        //{
        //    get
        //    {
        //        return editPosition ?? new RelayCommand(obj =>
        //        {
        //            Window window = obj as Window;
        //            string resultStr = "Не выбрана позиция";
        //            string noDepartmentStr = "Не выбран новый отдел";
        //            if (SelectedPosition != null)
        //            {
        //                if (PositionDepartment != null)
        //                {
        //                    resultStr = DataWorker.EditPosition(SelectedPosition, PositionName, PositionMaxNumber, PositionSalary, PositionDepartment);

        //                    UpdateAllDataView();
        //                    SetNullValuesToProperties();
        //                    ShowMessageToUser(resultStr);
        //                    window.Close();
        //                }
        //                else ShowMessageToUser(noDepartmentStr);
        //            }
        //            else ShowMessageToUser(resultStr);

        //        }
        //        );
        //    }
        //}

        //private RelayCommand editDepartment;
        //public RelayCommand EditDepartment
        //{
        //    get
        //    {
        //        return editDepartment ?? new RelayCommand(obj =>
        //        {
        //            Window window = obj as Window;
        //            string resultStr = "Не выбран отдел";
        //            if (SelectedDepartment != null)
        //            {
        //                resultStr = DataWorker.EditDepartment(SelectedDepartment, DepartmentName);

        //                UpdateAllDataView();
        //                SetNullValuesToProperties();
        //                ShowMessageToUser(resultStr);
        //                window.Close();
        //            }
        //            else ShowMessageToUser(resultStr);

        //        }
        //        );
        //    }
        //}
        //#endregion

        #region COMMANDS TO OPEN WINDOWS
        private RelayCommand openWorkWindow;
        public RelayCommand OpenWorkWindow
        {
            get
            {
                return openWorkWindow ?? new RelayCommand(obj =>
                {
                    OpenWorkWindowMethod();
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
        private RelayCommand openAddChoiceWindow;
        public RelayCommand OpenAddChoiceWindow
        {
            get
            {
                return openAddChoiceWindow ?? new RelayCommand(obj =>
                {
                    OpenAddChoiceWindowMethod();
                }
                );
            }
        }
        private RelayCommand openEditChoiceWindow;
        public RelayCommand OpenEditChoiceWindow
        {
            get
            {
                return openEditChoiceWindow ?? new RelayCommand(obj =>
                {
                    OpenEditChoiceWindowMethod();
                }
                );
            }
        }
        private RelayCommand openDeleteChoiceWindow;
        public RelayCommand OpenDeleteChoiceWindow
        {
            get
            {
                return openDeleteChoiceWindow ?? new RelayCommand(obj =>
                {
                    OpenDeleteChoiceWindowMethod();
                }
                );
            }
        }
        //private RelayCommand openEditItemWnd;
        //public RelayCommand OpenEditItemWnd
        //{
        //    get
        //    {
        //        return openEditItemWnd ?? new RelayCommand(obj =>
        //        {
        //            string resultStr = "Ничего не выбрано";
        //            //если сотрудник
        //            if (SelectedTabItem.Name == "UsersTab" && SelectedUser != null)
        //            {
        //                OpenEditUserWindowMethod(SelectedUser);
        //            }
        //            //если позиция
        //            if (SelectedTabItem.Name == "PositionsTab" && SelectedPosition != null)
        //            {
        //                OpenEditPositionWindowMethod(SelectedPosition);
        //            }
        //            //если отдел
        //            if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
        //            {
        //                OpenEditDepartmentWindowMethod(SelectedDepartment);
        //            }
        //        }
        //            );
        //    }
        //}
        #endregion

        #region METHODS TO OPEN WINDOW
        //методы открытия окон
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
        //окно удаления
        private void OpenDeleteChoiceWindowMethod()
        {
            DeleteChoiceWindow newDeleteChoiceWindow = new DeleteChoiceWindow();
            SetCenterPositionAndOpen(newDeleteChoiceWindow);
        }
        //окно редактирования
        private void OpenEditChoiceWindowMethod()
        {
            EditChoiceWindow newEditChoiceWindow = new EditChoiceWindow();
            SetCenterPositionAndOpen(newEditChoiceWindow);
        }
        //окно добавления
        private void OpenAddChoiceWindowMethod()
        {
            AddChoiceWindow newAddChoiceWindow = new AddChoiceWindow();
            SetCenterPositionAndOpen(newAddChoiceWindow);   
        }
        //окна редактирования
        //private void OpenEditDepartmentWindowMethod(Department department)
        //{
        //    EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow(department);
        //    SetCenterPositionAndOpen(editDepartmentWindow);
        //}
        //private void OpenEditPositionWindowMethod(Position position)
        //{
        //    EditPositionWindow editPositionWindow = new EditPositionWindow(position);
        //    SetCenterPositionAndOpen(editPositionWindow);
        //}
        //private void OpenEditUserWindowMethod(User user)
        //{
        //    EditUserWindow editUserWindow = new EditUserWindow(user);
        //    SetCenterPositionAndOpen(editUserWindow);
        //}
        private void SetCenterPositionAndOpen(Window window)
        {
            //window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }
        #endregion

        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        #region UPDATE VIEWS
        private void SetNullValuesToProperties()
        {
            //для Студента
            StudentFirstName = null;
            StudentFirstName = null;
            StudentFirstName = null;
            StudentGroup = null;
            //для Оценки
            GradeStudent = null;
            GradeDiscipline = null;
            GradeValue = 0;
            Date=DateTime.Today;
            //для Группы
            GroupName = null;
            Course = 0;
            //для Пользоваетля
            Login = null;
            Password = null;
        }
        private void UpdateInfoView()
        {
            UpdateStudentsInfo();
            //UpdateAllDisciplinesView();
            //UpdateAllGradesView();
            //UpdateAllGroupsView();
            //UpdateAllUserssView();
        }

        private void UpdateStudentsInfo()
        {
            AllStudents = DataWorker.GetAllStudents();
            WorkWindow.AllGradeInfoListView.ItemsSource = null;
            WorkWindow.AllGradeInfoListView.Items.Clear();
            WorkWindow.AllGradeInfoListView.ItemsSource = AllGrades;
            WorkWindow.AllGradeInfoListView.Items.Refresh();
        }
        //private void UpdateAllPositionsView()
        //{
        //    AllPositions = DataWorker.GetAllPositions();
        //    MainWindow.AllPositionsView.ItemsSource = null;
        //    MainWindow.AllPositionsView.Items.Clear();
        //    MainWindow.AllPositionsView.ItemsSource = AllPositions;
        //    MainWindow.AllPositionsView.Items.Refresh();
        //}
        //private void UpdateAllUsersView()
        //{
        //    AllUsers = DataWorker.GetAllUsers();
        //    MainWindow.AllUsersView.ItemsSource = null;
        //    MainWindow.AllUsersView.Items.Clear();
        //    MainWindow.AllUsersView.ItemsSource = AllUsers;
        //    MainWindow.AllUsersView.Items.Refresh();
        //}
        #endregion

        private void ShowMessageToUser(string message)
        {
            MessageBox.Show(message,"Сообщение", MessageBoxButton.OK,MessageBoxImage.Information);
            //MessageView messageView = new MessageView(message);
            //SetCenterPositionAndOpen(messageView);
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
