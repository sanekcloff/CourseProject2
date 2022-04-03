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

namespace WPF_EF_MVVM_SA_Proj.Resources.MVVM.Views
{
    public partial class SearchStudentWindow : Window
    {
        public static ComboBox SortStudentComboBox;
        public SearchStudentWindow()
        {
            InitializeComponent();
            SortStudentComboBox = StudentComboBox;
        }
    }
}
