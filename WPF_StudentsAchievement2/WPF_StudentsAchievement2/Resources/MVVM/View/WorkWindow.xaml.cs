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
using WPF_StudentsAchievement_Project.Resources.Profiles;
using WPF_StudentsAchievement_Project.Resources.Checks;


namespace WPF_StudentsAchievement_Project.Resources.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для WorkWindow.xaml
    /// </summary>
    public partial class WorkWindow : Window
    {
        DataBase dataBase = new DataBase();
        User user = new User();
        DataBaseField baseField = new DataBaseField();
        Check check = new Check();
        public WorkWindow()
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
        public string standartQuery = $"SELECT Students.ID,Students.StudentLastName,Students.StudentFirstName,Disciplines.DisciplineName,Grades.Grade,Grades.[Date] FROM Grades " +
            $"left join Disciplines on Disciplines.ID = Grades.DisciplineID " +
            $"left join Students on Students.ID = Grades.StudentID ";

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            LoginWindow loginWindow = new LoginWindow();
            RegistrationWindow registrationWindow = new RegistrationWindow();
            loginWindow.Close();
            registrationWindow.Close();
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        public void CheckComboBoxes()
        {
            if (GroupComboBox.Text.Length!=0 & StudentComboBox.Text.Length!=0 & DisciplineComboBox.Text.Length!=0)
            {
                InfoDataGridRefresh(standartQuery + $"WHERE Students.ID ='{baseField.StudentId}' AND Disciplines.ID='{baseField.DisciplineId}'");
            }
            else if(GroupComboBox.Text.Length!=0 & StudentComboBox.Text.Length==0 & DisciplineComboBox.Text.Length!=0)
            {
                InfoDataGridRefresh(standartQuery + $"WHERE Students.GroupID ='{baseField.GroupId}' AND Disciplines.ID='{baseField.DisciplineId}'");
            }
            else if (GroupComboBox.Text.Length != 0 & StudentComboBox.Text.Length != 0 & DisciplineComboBox.Text.Length == 0)
            {
                InfoDataGridRefresh(standartQuery + $"WHERE Students.ID ='{baseField.StudentId}'");
            }
            else if (GroupComboBox.Text.Length == 0 & StudentComboBox.Text.Length == 0 & DisciplineComboBox.Text.Length != 0)
            {
                InfoDataGridRefresh(standartQuery + $"WHERE Disciplines.ID ='{baseField.DisciplineId}'");
            }
            else if (GroupComboBox.Text.Length != 0 & StudentComboBox.Text.Length == 0 & DisciplineComboBox.Text.Length == 0)
            {
                InfoDataGridRefresh(standartQuery + $"WHERE Students.GroupID ='{baseField.GroupId}'");
            }
        }

        public void InfoDataGridRefresh(string queryString)
        {
            dataBase.openConnection();
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            command.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            InfoDataGrid.ItemsSource = table.DefaultView;
            adapter.Update(table);
            dataBase.closeConnection();
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
            dataBase.openConnection();
            string queryString = $"SELECT ID,GroupName FROM Groups WHERE ID='{check.SearchElementID(GroupComboBox.Text)}'";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            SqlDataReader sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {
                baseField.GroupId = sqlDataReader.GetInt32(0);
            }
            dataBase.closeConnection();
            CheckComboBoxes();
            StudentComboBoxRefresh();
            CheckGroupComboBox();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserName.Content = $"ПОЛЬЗОВАТЕЛЬ: {User.ShowUserLogin}";
            GroupComboBoxRefresh();
            DisciplineComboBoxRefresh();
            CheckGroupComboBox();

        }

        public void StudentComboBoxRefresh()
        {
            dataBase.openConnection();
            string queryString = $"SELECT ID, StudentLastName,StudentFirstName, GroupID FROM Students WHERE GroupID='{baseField.GroupId}'";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            SqlDataReader sqlDataReader = command.ExecuteReader();
            StudentComboBox.Items.Clear();
            while (sqlDataReader.Read())
            {
                int Id = sqlDataReader.GetInt32(0);
                string LastName = sqlDataReader.GetString(1);
                string FirstName = sqlDataReader.GetString(2);
                StudentComboBox.Items.Add($"{Id}: {LastName} {FirstName}");
            }
            dataBase.closeConnection();
        }
        private void StudentComboBox_DropDownClosed(object sender, EventArgs e)
        {
            dataBase.openConnection();
            string queryString = $"SELECT ID FROM Students WHERE ID='{check.SearchElementID(StudentComboBox.Text)}'";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            SqlDataReader sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {
                baseField.StudentId = sqlDataReader.GetInt32(0);
            }
            dataBase.closeConnection();
            CheckComboBoxes();
        }
        public void DisciplineComboBoxRefresh()
        {
            dataBase.openConnection();
            string queryString = $"SELECT ID, DisciplineName FROM Disciplines";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            SqlDataReader sqlDataReader = command.ExecuteReader();
            DisciplineComboBox.Items.Clear();
            while (sqlDataReader.Read())
            {
                int Id = sqlDataReader.GetInt32(0);
                string DisciplineName = sqlDataReader.GetString(1);
                DisciplineComboBox.Items.Add($"{Id}: {DisciplineName}");
            }
            dataBase.closeConnection();
        }

        private void DisciplineComboBox_DropDownClosed(object sender, EventArgs e)
        {
            dataBase.openConnection();
            string queryString = $"SELECT ID FROM Disciplines WHERE ID='{check.SearchElementID(DisciplineComboBox.Text)}'";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            SqlDataReader sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {
                baseField.DisciplineId = sqlDataReader.GetInt32(0);
            }
            dataBase.closeConnection();
            CheckComboBoxes();
        }
        
        public void CheckGroupComboBox()
        {
            if (baseField.GroupId==0)
            {
                StudentComboBox.IsEnabled = false;
            }
            else
            {
                StudentComboBox.IsEnabled = true;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddChoiceWindow addChoiceWindow = new AddChoiceWindow();
            addChoiceWindow.Show();
            this.Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditChoiceWindow editChoiceWindow = new EditChoiceWindow();
            editChoiceWindow.Show();
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteChoiceWindow deleteChoiceWindow = new DeleteChoiceWindow();
            deleteChoiceWindow.Show();
            this.Close();
        }

    }
}
