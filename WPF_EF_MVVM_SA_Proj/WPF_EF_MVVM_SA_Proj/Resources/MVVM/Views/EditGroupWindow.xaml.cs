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
using WPF_EF_MVVM_SA_Proj.Resources.MVVM.Models;
using WPF_EF_MVVM_SA_Proj.Resources.MVVM.ViewModels;

namespace WPF_EF_MVVM_SA_Proj.Resources.MVVM.Views
{
    /// <summary>
    /// Логика взаимодействия для AddGroupWindow.xaml
    /// </summary>
    public partial class EditGroupWindow : Window
    {
        public EditGroupWindow(Group groupToEdit)
        {
            InitializeComponent();
            DataManageVM.SelectedGroup = groupToEdit;
            this.GroupTextBox.Text = groupToEdit.GroupName;
            DataManageVM.GroupName = groupToEdit.GroupName;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DragDropBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
