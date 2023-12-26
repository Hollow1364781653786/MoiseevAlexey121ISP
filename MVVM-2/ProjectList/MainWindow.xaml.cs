using ProjectList.Models;
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

namespace ProjectList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mvvm.projects = new() { new Project( "tsadest", "testint desc",  DateTime.Now, DateTime.Now), new Project("test", "testint desc", DateTime.Now, DateTime.Now) };

            mainFrame.Navigate(new Pages.ProjectsList(mvvm));

            mvvm.projects[0].Description = "dasd";
             
        }

        private MVVM mvvm = new();





    }
}
