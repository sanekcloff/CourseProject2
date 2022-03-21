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
    /// Логика взаимодействия для DisciplineView.xaml
    /// </summary>
    public partial class AddDisciplineView : UserControl
    {
        DataBase dataBase = new DataBase();
        User user = new User();
        DataBaseField baseField = new DataBaseField();
        Check check = new Check();
        public AddDisciplineView()
        {
            InitializeComponent();
        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            if (baseField.DisciplineName.Length!=0)
            {
                if (check.Discipline(baseField.DisciplineName) == false)
                {
                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        DataTable table = new DataTable();
                        string queryString = $"INSERT INTO [Disciplines]([DisciplineName]) VALUES ('{baseField.DisciplineName}')";
                        SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
                        dataBase.openConnection();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show($"Дисциплина добавлена!", "УСПЕШНО", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("ОШИБКА ДОБАВЛЕНИЯ ДИСИПЛИНЫ:", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void DisciplineTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            baseField.DisciplineName = DisciplineTextBox.Text;
        }
    }
}
