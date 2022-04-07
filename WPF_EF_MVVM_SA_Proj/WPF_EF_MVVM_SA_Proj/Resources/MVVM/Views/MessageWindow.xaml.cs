﻿using System;
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
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow(string message)
        {
            InitializeComponent();
            MessageText.Text=message;
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
