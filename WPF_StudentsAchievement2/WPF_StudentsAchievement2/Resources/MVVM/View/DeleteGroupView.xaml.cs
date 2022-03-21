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
    /// Логика взаимодействия для DeleteGroupView.xaml
    /// </summary>
    public partial class DeleteGroupView : UserControl
    {
        DataBase dataBase = new DataBase();
        User user = new User();
        DataBaseField baseField = new DataBaseField();
        Check check = new Check();
        public DeleteGroupView()
        {
            InitializeComponent();
        }
        public void GroupIdComboBoxRefrsh()
        {
            dataBase.openConnection();
            string queryString = "SELECT ID,GroupName,Course FROM Groups";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            SqlDataReader sqlDataReader = command.ExecuteReader();
            GroupIdComboBox.Items.Clear();
            while (sqlDataReader.Read())
            {
                int GroupId = sqlDataReader.GetInt32(0);
                string GroupName = sqlDataReader.GetString(1);
                int Course = sqlDataReader.GetInt32(2);
                GroupIdComboBox.Items.Add($"{GroupId}: {GroupName} Курс №{Course}");
            }
            dataBase.closeConnection();
        }
        private void DeleteGroupView1_Loaded(object sender, RoutedEventArgs e)
        {
            GroupIdComboBoxRefrsh();
            CheckStudents();
        }

        private void GroupIdComboBox_DropDownClosed(object sender, EventArgs e)
        {
            baseField.GroupId = Convert.ToInt32(check.SearchElementID(GroupIdComboBox.Text));
            CheckStudents();
        }
        public void CheckStudents()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = $"SELECT * FROM Students WHERE GroupId={baseField.GroupId}";

            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                WarningTextBlock.Visibility = Visibility.Visible;
                ReadyButton.IsEnabled = false;
            }
            else
            {
                WarningTextBlock.Visibility= Visibility.Collapsed;
                ReadyButton.IsEnabled = true;
            }
        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            if (baseField.GroupId != 0)
            {
                try
                {
                    string queryString = $"DELETE FROM [Groups] WHERE ID={baseField.GroupId}";
                    SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
                    dataBase.openConnection();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show($"Группа удалена!", "УСПЕШНО", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("ОШИБКА УДАЛЕНИЯ ГРУППЫ:", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    dataBase.closeConnection();
                    GroupIdComboBoxRefrsh();
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
