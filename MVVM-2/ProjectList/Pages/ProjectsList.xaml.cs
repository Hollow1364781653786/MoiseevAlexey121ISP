﻿using ProjectList.Models;
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

namespace ProjectList.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProjectsList.xaml
    /// </summary>
    public partial class ProjectsList : Page
    {
        internal ProjectsList(MVVM mvvm)
        {
            InitializeComponent();
            DataContext = mvvm;
        }

        private void View_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void View_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
