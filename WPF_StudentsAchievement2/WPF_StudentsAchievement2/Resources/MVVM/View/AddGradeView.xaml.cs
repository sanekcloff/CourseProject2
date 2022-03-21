using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_StudentsAchievement_Project.Resources.Checks;
using WPF_StudentsAchievement_Project.Resources.Profiles;
using System.Data;
using System.Data.SqlClient;

namespace WPF_StudentsAchievement_Project.Resources.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для GradeView.xaml
    /// </summary>
    public partial class AddGradeView : UserControl
    {
        DataBase dataBase = new DataBase();
        User user = new User();
        DataBaseField baseField = new DataBaseField();
        Check check = new Check();
        public AddGradeView()
        {
            InitializeComponent();
        }

        private void GradeViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            DisciplineComboBoxRefresh();
            GroupComboBoxRefresh();
            CheckGroupComboBox();
            StudentComboBoxRefresh();
        }
        public void DisciplineComboBoxRefresh()
        {
            dataBase.openConnection();
            string queryString = $"SELECT ID, DisciplineName FROM Disciplines";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            SqlDataReader sqlDataReader = command.ExecuteReader();
            DisciplineComboBox.Items.Clear();
            while (sqlDataReader.Read())
            {
                int Id = sqlDataReader.GetInt32(0);
                string DisciplineName = sqlDataReader.GetString(1);
                DisciplineComboBox.Items.Add($"{Id}: {DisciplineName}");
            }
            dataBase.closeConnection();
        }
        public void StudentComboBoxRefresh()
        {
            dataBase.openConnection();
            string queryString = $"SELECT ID, StudentLastName,StudentFirstName, GroupID FROM Students WHERE GroupID='{baseField.GroupId}'";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            SqlDataReader sqlDataReader = command.ExecuteReader();
            StudentComboBox.Items.Clear();
            while (sqlDataReader.Read())
            {
                int Id = sqlDataReader.GetInt32(0);
                string LastName = sqlDataReader.GetString(1);
                string FirstName = sqlDataReader.GetString(2);
                StudentComboBox.Items.Add($"{Id}: {LastName} {FirstName}");
            }
            dataBase.closeConnection();
        }
        public void GroupComboBoxRefresh()
        {
            dataBase.openConnection();
            string queryString = "SELECT ID,GroupName FROM Groups";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            SqlDataReader sqlDataReader = command.ExecuteReader();
            GroupComboBox.Items.Clear();
            while (sqlDataReader.Read())
            {
                int GroupId = sqlDataReader.GetInt32(0);
                string GroupName = sqlDataReader.GetString(1);
                GroupComboBox.Items.Add($"{GroupId}: {GroupName}");
            }
            dataBase.closeConnection();
        }
        public void CheckGroupComboBox()
        {
            if (baseField.GroupId == 0)
            {
                StudentComboBox.IsEnabled = false;
            }
            else
            {
                StudentComboBox.IsEnabled = true;
            }
        }

        private void DisciplineComboBox_DropDownClosed(object sender, EventArgs e)
        {
            baseField.DisciplineId = Convert.ToInt32(check.SearchElementID(DisciplineComboBox.Text));
        }

        private void GroupComboBox_DropDownClosed(object sender, EventArgs e)
        {
            baseField.GroupId = Convert.ToInt32(check.SearchElementID(GroupComboBox.Text));
            StudentComboBoxRefresh();
            CheckGroupComboBox();
        }

        private void StudentComboBox_DropDownClosed(object sender, EventArgs e)
        {
            baseField.StudentId = Convert.ToInt32(check.SearchElementID(StudentComboBox.Text));
        }

        private void GradeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            baseField.Grade = Convert.ToInt32(GradeComboBox.Text);
        }

        private void DatePick_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                baseField.Date = DatePick.SelectedDate.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            if (baseField.StudentId!=0 & baseField.DisciplineId!=0 & baseField.Grade!=0 & baseField.Date.Day !=0 & baseField.Date.Month!=0)
            {
                try
                {
                    string queryString = $"INSERT INTO [Grades]([StudentID],[DisciplineID],[Grade],[Date]) VALUES ({baseField.StudentId},{baseField.DisciplineId},{baseField.Grade},'{baseField.Date.Year}-{baseField.Date.Month}-{baseField.Date.Day}')";
                    SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
                    dataBase.openConnection();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show($"Оценка выставлена!", "УСПЕШНО", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("ОШИБКА ВЫСТАВЛЕНИЯ ОЦЕНКИ:", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    dataBase.closeConnection();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Заполните поля!", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
