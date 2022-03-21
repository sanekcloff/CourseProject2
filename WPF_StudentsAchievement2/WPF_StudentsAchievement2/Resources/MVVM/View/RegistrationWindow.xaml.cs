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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using WPF_StudentsAchievement_Project.Resources.Checks;
using WPF_StudentsAchievement_Project.Resources.Profiles;

namespace WPF_StudentsAchievement_Project.Resources.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        Check check = new Check();
        User user = new User();
        DataBase dataBase = new DataBase();

        public RegistrationWindow()
        {
            InitializeComponent();
        }
        private void MovingWindow(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            user.Login = LoginTextBox.Text;
            user.Password = PasswordTextBox.Text;
            try
            {
                if (check.User(user.Login, user.Password) == false)
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataTable table = new DataTable();

                    string queryString = $"INSERT INTO Register VALUES('{user.Login}', '{user.Password}')";

                    SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

                    dataBase.openConnection();

                    if (command.ExecuteNonQuery()== 1)
                    {
                        MessageBox.Show($"Аккаунт {user.Login} успешно зарегистрирован!", "УСПЕШНО",MessageBoxButton.OK,MessageBoxImage.Information);
                        LoginWindow loginWindow = new LoginWindow();
                        this.Close();
                        loginWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("ОШИБКА СОЗДАНИЯ АККАУНТА:", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    dataBase.closeConnection();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            this.Hide();
            loginWindow.Show();

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Close();
        }
    }
}
