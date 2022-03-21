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
    /// Логика взаимодействия для EditStudentView.xaml
    /// </summary>
    public partial class EditStudentView : UserControl
    {
        DataBase dataBase = new DataBase();
        User user = new User();
        DataBaseField baseField = new DataBaseField();
        Check check = new Check();
        public EditStudentView()
        {
            InitializeComponent();
        }

        public void StudentsIdComboBoxRefresh()
        {
            dataBase.openConnection();
            string queryString = $"SELECT Students.ID, Students.StudentLastName,Students.StudentFirstName,Students.StudentMiddleName,Groups.GroupName FROM Students " +
                $"left join Groups on Groups.ID = Students.GroupID";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            SqlDataReader sqlDataReader = command.ExecuteReader();
            StudentsIdComboBox.Items.Clear();
            while (sqlDataReader.Read())
            {
                int Id = sqlDataReader.GetInt32(0);
                string LastName = sqlDataReader.GetString(1);
                string FirstName = sqlDataReader.GetString(2);
                string MiddleName = sqlDataReader.GetString(3);
                string GroupId = sqlDataReader.GetString(4);
                StudentsIdComboBox.Items.Add($"{Id}: {LastName} {FirstName} {MiddleName} ({GroupId})");
            }
            dataBase.closeConnection();
        }
        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                string queryString = $"UPDATE [Students] SET [StudentLastName] = '{baseField.StudentLastName}',[StudentFirstName] = '{baseField.StudentFirstName}',[StudentMiddleName] = '{baseField.StudentMiddleName}',[GroupID] ={baseField.GroupId} WHERE ID={baseField.StudentId}";
                SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
                dataBase.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show($"Студент изменён!", "УСПЕШНО", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("ОШИБКА ИЗМЕНЕНИЯ СТУДЕНТА:", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                dataBase.closeConnection();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            StudentsIdComboBoxRefresh();
            GroupComboBoxRefresh();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            StudentsIdComboBoxRefresh();
            GroupComboBoxRefresh();
        }

        private void StudentsIdComboBox_DropDownClosed(object sender, EventArgs e)
        {
            baseField.StudentId = Convert.ToInt32(check.SearchElementID(StudentsIdComboBox.Text));
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
        private void GroupComboBox_DropDownClosed(object sender, EventArgs e)
        {
            baseField.GroupId = Convert.ToInt32(check.SearchElementID(GroupComboBox.Text));
        }

        private void LastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            baseField.StudentLastName = LastNameTextBox.Text;
        }

        private void FirstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            baseField.StudentFirstName = FirstNameTextBox.Text;
        }

        private void MiddleNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            baseField.StudentMiddleName = MiddleNameTextBox.Text;
        }
    }
}
