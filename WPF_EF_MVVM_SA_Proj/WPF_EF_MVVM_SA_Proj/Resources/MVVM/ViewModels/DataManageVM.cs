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

        #region METHODS TO OPEN WINDOW

        protected void SetCenterPositionAndOpen(Window window)
        {
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }
        #endregion

        #region UPDATE VIEWS
        public static void SetNullValuesToProperties()
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
        protected void UpdateInfoView()
        {
            UpdateWWInfoView();
            UpdateDisciplinesInfo();
            UpdateGroupsInfo();
            UpdateStudentsInfo();
            UpdateGradesEDWInfo();
        }
        protected void UpdateWWInfoView()
        {
            UpdateGradesWWInfo();
        }
        protected void UpdateGradesWWInfo()
        {
            AllGrades = DataWorker.GetAllGrades();
            WorkWindow.AllGradeInfoListView.ItemsSource = null;
            WorkWindow.AllGradeInfoListView.Items.Clear();
            WorkWindow.AllGradeInfoListView.ItemsSource = AllGrades;
            WorkWindow.AllGradeInfoListView.Items.Refresh();
        }
        protected void UpdateGradesWWInfoSortedByStudent()
        {
            AllGradesByStudentId = DataWorker.GetAllGradesByStudentId(StudentInf.Id);
            WorkWindow.AllGradeInfoListView.ItemsSource = null;
            WorkWindow.AllGradeInfoListView.Items.Clear();
            WorkWindow.AllGradeInfoListView.ItemsSource = AllGradesByStudentId;
            WorkWindow.AllGradeInfoListView.Items.Refresh();
        }
        protected void UpdateGradesEDWInfo()
        {
            AllGrades = DataWorker.GetAllGrades();
            DeleteEditWindow.AllGradeInfoListView.ItemsSource = null;
            DeleteEditWindow.AllGradeInfoListView.Items.Clear();
            DeleteEditWindow.AllGradeInfoListView.ItemsSource = AllGrades;
            DeleteEditWindow.AllGradeInfoListView.Items.Refresh();
        }
        protected void UpdateGroupsInfo()
        {
            AllGroups = DataWorker.GetAllGroups();
            DeleteEditWindow.AllGroupInfoListView.ItemsSource = null;
            DeleteEditWindow.AllGroupInfoListView.Items.Clear();
            DeleteEditWindow.AllGroupInfoListView.ItemsSource = AllGroups;
            DeleteEditWindow.AllGroupInfoListView.Items.Refresh();
        }
        protected void UpdateStudentsInfo()
        {
            AllStudents = DataWorker.GetAllStudents();
            DeleteEditWindow.AllStudentInfoListView.ItemsSource = null;
            DeleteEditWindow.AllStudentInfoListView.Items.Clear();
            DeleteEditWindow.AllStudentInfoListView.ItemsSource = AllStudents;
            DeleteEditWindow.AllStudentInfoListView.Items.Refresh();
        }
        protected void UpdateDisciplinesInfo()
        {
            AllDisciplines = DataWorker.GetAllDisciplines();
            DeleteEditWindow.AllDisciplineInfoListView.ItemsSource = null;
            DeleteEditWindow.AllDisciplineInfoListView.Items.Clear();
            DeleteEditWindow.AllDisciplineInfoListView.ItemsSource = AllDisciplines;
            DeleteEditWindow.AllDisciplineInfoListView.Items.Refresh();
        }
        #endregion

        public static void ShowMessageToUser(string message)
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
