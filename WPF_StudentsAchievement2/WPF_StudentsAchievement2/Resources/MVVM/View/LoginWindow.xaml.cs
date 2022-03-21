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
using WPF_StudentsAchievement_Project.Resources.Profiles;
using WPF_StudentsAchievement_Project.Resources.MVVM.View;
using WPF_StudentsAchievement_Project.Resources.Checks;
using System.Data;
using System.Data.SqlClient;

namespace WPF_StudentsAchievement_Project
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        Check check = new Check();
        User user = new User();
        DataBase dataBase = new DataBase();
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Hide();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            user.Login = LoginTextBox.Text;
            user.Password = PasswordTextBox.Password;
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();

                string queryString = $"SELECT Login,Password FROM Register WHERE Login='{user.Login}' AND  Password='{user.Password}'";

                SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

                dataBase.openConnection();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count == 1)
                {
                    MessageBox.Show($"Вход в аккаунт {user.Login}!", "УСПЕШНО", MessageBoxButton.OK, MessageBoxImage.Information);
                    User.ShowUserLogin = user.Login;
                    WorkWindow workWindow = new WorkWindow();
                    workWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Такого аккаунта не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                dataBase.closeConnection();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void MovingWindow(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
