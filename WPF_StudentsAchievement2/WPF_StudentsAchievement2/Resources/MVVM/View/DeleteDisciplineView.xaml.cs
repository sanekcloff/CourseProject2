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
    /// Логика взаимодействия для DeleteDisciplineView.xaml
    /// </summary>
    public partial class DeleteDisciplineView : UserControl
    {
        DataBase dataBase = new DataBase();
        User user = new User();
        DataBaseField baseField = new DataBaseField();
        Check check = new Check();
        public DeleteDisciplineView()
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

        private void DeleteDisciplineView1_Loaded(object sender, RoutedEventArgs e)
        {
            DisciplineComboBoxRefresh();
        }

        private void DisciplineIdComboBox_DropDownClosed(object sender, EventArgs e)
        {
            baseField.DisciplineId = Convert.ToInt32(check.SearchElementID(DisciplineIdComboBox.Text));
        }

        public void DeleteAllDisciplineGrades()
        {
            try
            {
                string queryString = $"DELETE FROM [Grades] WHERE DisciplineID={baseField.DisciplineId}";
                SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
                dataBase.openConnection();
                if (command.ExecuteNonQuery() != 0)
                {
                    MessageBox.Show($"Оценки по дисциплине удалены!", "УСПЕШНО", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Оценки по дисциплине не найдены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                dataBase.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            if (baseField.DisciplineId != 0)
            {
                try
                {
                    DeleteAllDisciplineGrades();
                    string queryString = $"DELETE FROM [Disciplines] WHERE ID={baseField.DisciplineId}";
                    SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
                    dataBase.openConnection();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show($"Дисциплина удалена!", "УСПЕШНО", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("ОШИБКА УДАЛЕНИЯ ДИСЦИПЛИНЫ:", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    dataBase.closeConnection();
                    DisciplineComboBoxRefresh();
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
