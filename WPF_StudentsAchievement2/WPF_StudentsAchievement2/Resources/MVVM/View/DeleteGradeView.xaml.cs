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
    /// Логика взаимодействия для DeleteGradeView.xaml
    /// </summary>
    public partial class DeleteGradeView : UserControl
    {
        DataBase dataBase = new DataBase();
        User user = new User();
        DataBaseField baseField = new DataBaseField();
        Check check = new Check();
        public DeleteGradeView()
        {
            InitializeComponent();
        }

        public void DisciplineComboBoxRefresh()
        {
            dataBase.openConnection();
            string queryString = $"SELECT ID, DisciplineName FROM Disciplines";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            SqlDataReader sqlDataReader = command.ExecuteReader();
            DisciplineIdComboBox.Items.Clear();
            while (sqlDataReader.Read())
            {
                int Id = sqlDataReader.GetInt32(0);
                string DisciplineName = sqlDataReader.GetString(1);
                DisciplineIdComboBox.Items.Add($"{Id}: {DisciplineName}");
            }
            dataBase.closeConnection();
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
        public void GradeComboBoxRefresh()
        {
            dataBase.openConnection();
            string queryString = $"SELECT ID,Grade, Date FROM Grades WHERE StudentId={baseField.StudentId} AND DisciplineID={baseField.DisciplineId}";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            SqlDataReader sqlDataReader = command.ExecuteReader();
            GradeIdComboBox.Items.Clear();
            while (sqlDataReader.Read())
            {
                int Id = sqlDataReader.GetInt32(0);
                int Grade = sqlDataReader.GetInt32(1);
                DateTime Date = sqlDataReader.GetDateTime(2);
                GradeIdComboBox.Items.Add($"{Id}: {Grade} ({Date})");
            }
            dataBase.closeConnection();
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
        public void CheckStudentComboBox()
        {
            if (baseField.StudentId == 0)
            {
                GradeIdComboBox.IsEnabled = false;
            }
            else
            {
                GradeIdComboBox.IsEnabled = true;
            }
        }
        private void DeleteGradeView1_Loaded(object sender, RoutedEventArgs e)
        {
            DisciplineComboBoxRefresh();
            GroupComboBoxRefresh();
            StudentComboBoxRefresh();
            GradeComboBoxRefresh();
            CheckGroupComboBox();
            CheckStudentComboBox();
        }
        private void DisciplineComboBox_DropDownClosed(object sender, EventArgs e)
        {
            baseField.DisciplineId = Convert.ToInt32(check.SearchElementID(DisciplineIdComboBox.Text));
            GradeComboBoxRefresh();
        }

        private void StudentIdComboBox_DropDownClosed(object sender, EventArgs e)
        {
            baseField.StudentId = Convert.ToInt32(check.SearchElementID(StudentIdComboBox.Text));
            CheckStudentComboBox();
            GradeComboBoxRefresh();
        }

        private void GroupIdComboBox_DropDownClosed(object sender, EventArgs e)
        {
            baseField.GroupId = Convert.ToInt32(check.SearchElementID(GroupIdComboBox.Text));
            StudentComboBoxRefresh();
            CheckGroupComboBox();
        }

        private void GradeIdComboBox_DropDownClosed(object sender, EventArgs e)
        {
            baseField.GradeId = Convert.ToInt32(check.SearchElementID(GradeIdComboBox.Text));
        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            if (baseField.StudentId != 0 & baseField.DisciplineId != 0 & baseField.GradeId != 0)
            {
                try
                {
                    string queryString = $"DELETE FROM [Grades] WHERE ID={baseField.GradeId}";
                    SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
                    dataBase.openConnection();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show($"Оценка удалена!", "УСПЕШНО", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("ОШИБКА УДАЛЕНИЯ ОЦЕНКИ:", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
