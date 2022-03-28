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
    /// Логика взаимодействия для AddGradeWindow.xaml
    /// </summary>
    public partial class EditGradeWindow : Window
    {
        public EditGradeWindow(Grade gradeToEdit)
        {
            InitializeComponent();
            DataManageVM.SelectedGrade = gradeToEdit;
            DataManageVM.GradeStudent = gradeToEdit.GradeStudent;
            DataManageVM.GradeDiscipline = gradeToEdit.GradeDiscipline;
            DataManageVM.GradeValue = gradeToEdit.GradeValue;
            DataManageVM.Date = gradeToEdit.Date;
        }
    }
}
