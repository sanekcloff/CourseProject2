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
using System.Data;
using System.Data.SqlClient;
using WPF_StudentsAchievement_Project.Resources.Profiles;
using WPF_StudentsAchievement_Project.Resources.Checks;

namespace WPF_StudentsAchievement_Project.Resources.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для StudentView.xaml
    /// </summary>
    public partial class AddStudentView : UserControl
    {
        DataBase dataBase = new DataBase();
        User user = new User();
        DataBaseField baseField = new DataBaseField();
        Check check = new Check();
        public AddStudentView()
        {
            InitializeComponent();
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

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            if (baseField.StudentFirstName.Length != 0 & baseField.StudentLastName.Length != 0 & baseField.StudentMiddleName.Length != 0 & baseField.GroupId != 0)
            {
                if (check.Student(baseField.StudentLastName,baseField.StudentFirstName,baseField.StudentMiddleName) == false)
                {
                    try
                    {
                        string queryString = $"INSERT INTO [Students]([StudentLastName],[StudentFirstName],[StudentMiddleName],[GroupID]) VALUES ('{baseField.StudentLastName}','{baseField.StudentFirstName}','{baseField.StudentMiddleName}',{baseField.GroupId})";
                        SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
                        dataBase.openConnection();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show($"Студент добавлен!", "УСПЕШНО", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("ОШИБКА ДОБАВЛЕНИЯ СТУДЕНТА:", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        dataBase.closeConnection();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните поля!", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GroupComboBoxRefresh();
        }
    }
}
