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

namespace WPF_StudentsAchievement_Project.Resources.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для EditChoiceWindow.xaml
    /// </summary>
    public partial class EditChoiceWindow : Window
    {
        public EditChoiceWindow()
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            WorkWindow workWindow = new WorkWindow();
            this.Close();
            workWindow.Show();
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
