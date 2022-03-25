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
using WPF_StudentsAchievement2.Resources.MVVM.ViewModel;


namespace WPF_StudentsAchievement2.Resources.MVVM.View
{
    public partial class WorkWindow : Window
    {
        public static ListView AllGradeInfoListView;
        public WorkWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            AllGradeInfoListView = GradeInfoListView;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState=WindowState.Minimized;
        }
    }
}
