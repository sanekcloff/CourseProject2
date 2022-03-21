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
    /// Логика взаимодействия для DeleteStudentView.xaml
    /// </summary>
    public partial class DeleteStudentView : UserControl
    {
        DataBase dataBase = new DataBase();
        User user = new User();
        DataBaseField baseField = new DataBaseField();
        Check check = new Check();
        public DeleteStudentView()
        {
            InitializeComponent();
        }
        public void DeleteAllStudentGrades()
        {
            try
            {
                string queryString = $"DELETE FROM [Grades] WHERE StudentID={baseField.StudentId}";
                SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
                dataBase.openConnection();
                if (command.ExecuteNonQuery() != 0)
                {
                    MessageBox.Show($"Оценки студента удалены!", "УСПЕШНО", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Оценки студента не найдены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                dataBase.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            if (baseField.StudentId != 0 & baseField.GroupId !=0)
            {
                try
                {
                    DeleteAllStudentGrades();
                    string queryString = $"DELETE FROM [Students] WHERE ID={baseField.StudentId}";
                    SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
                    dataBase.openConnection();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show($"Студент удалён!", "УСПЕШНО", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("ОШИБКА УДАЛЕНИЯ СТУДЕНТА:", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    dataBase.closeConnection();
                    GroupComboBoxRefresh();
                    StudentComboBoxRefresh();
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
        public void StudentComboBoxRefresh()
        {
            dataBase.openConnection();
            string queryString = $"SELECT ID, StudentLastName,StudentFirstName, GroupID FROM Students WHERE GroupID='{baseField.GroupId}'";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            SqlDataReader sqlDataReader = command.ExecuteReader();
            StudentIdComboBox.Items.Clear();
            while (sqlDataReader.Read())
            {
                int Id = sqlDataReader.GetInt32(0);
                string LastName = sqlDataReader.GetString(1);
                string FirstName = sqlDataReader.GetString(2);
                StudentIdComboBox.Items.Add($"{Id}: {LastName} {FirstName}");
            }
            dataBase.closeConnection();
        }
        public void GroupComboBoxRefresh()
        {
            dataBase.openConnection();
            string queryString = "SELECT ID,GroupName FROM Groups";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            SqlDataReader sqlDataReader = command.ExecuteReader();
            GroupIdComboBox.Items.Clear();
            while (sqlDataReader.Read())
            {
                int GroupId = sqlDataReader.GetInt32(0);
                string GroupName = sqlDataReader.GetString(1);
                GroupIdComboBox.Items.Add($"{GroupId}: {GroupName}");
            }
            dataBase.closeConnection();
        }
        private void GroupIdComboBox_DropDownClosed(object sender, EventArgs e)
        {
            baseField.GroupId = Convert.ToInt32(check.SearchElementID(GroupIdComboBox.Text));
            StudentComboBoxRefresh();
            CheckGroupComboBox();
        }

        private void StudentIdComboBox_DropDownClosed(object sender, EventArgs e)
        {
            baseField.StudentId = Convert.ToInt32(check.SearchElementID(StudentIdComboBox.Text));
        }
        public void CheckGroupComboBox()
        {
            if (baseField.GroupId == 0)
            {
                StudentIdComboBox.IsEnabled = false;
            }
            else
            {
                StudentIdComboBox.IsEnabled = true;
            }
        }

        private void DeleteStudentView1_Loaded(object sender, RoutedEventArgs e)
        {
            GroupComboBoxRefresh();
            CheckGroupComboBox();
            StudentComboBoxRefresh();
        }
    }
}
