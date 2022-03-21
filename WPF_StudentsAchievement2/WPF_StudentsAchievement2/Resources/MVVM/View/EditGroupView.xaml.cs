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
    /// Логика взаимодействия для EditGroupView.xaml
    /// </summary>
    public partial class EditGroupView : UserControl
    {
        DataBase dataBase = new DataBase();
        User user = new User();
        DataBaseField baseField = new DataBaseField();
        Check check = new Check();
        public EditGroupView()
        {
            InitializeComponent();
        }

        public void GroupIdComboBoxRefresh()
        {
            dataBase.openConnection();
            string queryString = $"SELECT ID,GroupName,Course FROM Groups";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            SqlDataReader sqlDataReader = command.ExecuteReader();
            GroupIdComboBox.Items.Clear();
            while (sqlDataReader.Read())
            {
                int Id = sqlDataReader.GetInt32(0);
                string GroupName = sqlDataReader.GetString(1);
                int Course = sqlDataReader.GetInt32(2);
                GroupIdComboBox.Items.Add($"{Id}: {GroupName} {Course}-ий курс");
            }
            dataBase.closeConnection();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GroupIdComboBoxRefresh();
        }

        private void GroupIdComboBox_DropDownClosed(object sender, EventArgs e)
        {
            baseField.GroupId = Convert.ToInt32(check.SearchElementID(GroupIdComboBox.Text));
        }

        private void CourseComboBox_DropDownClosed(object sender, EventArgs e)
        {
            baseField.Course = Convert.ToInt32(CourseComboBox.Text);
        }

        private void GroupNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            baseField.GroupName = GroupNameTextBox.Text;
        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                string queryString = $"UPDATE [Groups] SET [GroupName] ='{baseField.GroupName}',[Course] ={baseField.Course} WHERE ID={baseField.GroupId}";
                SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
                dataBase.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show($"Группа изменена!", "УСПЕШНО", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("ОШИБКА ИЗМЕНЕНИЯ ГРУППЫ:", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                dataBase.closeConnection();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            GroupIdComboBoxRefresh();
        }
    }
}
