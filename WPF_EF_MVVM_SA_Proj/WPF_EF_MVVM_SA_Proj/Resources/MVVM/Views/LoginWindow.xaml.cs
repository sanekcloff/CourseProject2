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
using WPF_EF_MVVM_SA_Proj.Resources.MVVM.Models.Data;
using WPF_EF_MVVM_SA_Proj.Resources.MVVM.ViewModels;

namespace WPF_EF_MVVM_SA_Proj.Resources.MVVM.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            //using (ApplicationContext context = new ApplicationContext())
            //{
            //    context.Database.EnsureCreated();
            //}

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
