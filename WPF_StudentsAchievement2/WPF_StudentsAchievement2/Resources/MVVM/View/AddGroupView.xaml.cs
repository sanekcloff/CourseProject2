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
    /// Логика взаимодействия для GroupView.xaml
    /// </summary>
    public partial class AddGroupView : UserControl
    {
        DataBase dataBase = new DataBase();
        User user = new User();
        DataBaseField baseField = new DataBaseField();
        Check check = new Check();
        public AddGroupView()
        {
            InitializeComponent();
        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            if (baseField.GroupName.Length != 0 & baseField.Course != 0)
            {
                if (check.Group(baseField.GroupName) == false)
                {
                    try
                    {
                        string queryString = $"INSERT INTO [Groups]([GroupName],[Course]) VALUES ('{baseField.GroupName}',{baseField.Course})";
                        SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
                        dataBase.openConnection();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show($"Группа добавлена!", "УСПЕШНО", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("ОШИБКА ДОБАВЛЕНИЯ ГРУППЫ:", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void GroupTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            baseField.GroupName = GroupTextBox.Text;
        }

        private void CourseComboBox_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                baseField.Course = Convert.ToInt32(CourseComboBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
