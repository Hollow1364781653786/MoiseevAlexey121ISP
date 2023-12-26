using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace ProjectList.Models
{
    internal class MVVM : INotifyPropertyChanged
    {
        public ObservableCollection<Project> projects { get; set; }
        private Project _selectedProject;

        public Project SelectedProject { get { return _selectedProject; } set { _selectedProject = value; OnPropertyChanged("SelectedProject"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public Command AddCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    try
                    {
                        this.projects.Add(new Project("Новый проект", "описание нового проекта", DateTime.Today, DateTime.MaxValue));
                    }
                    catch { }
                });
            }
        }
    }

    public class Command : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Command(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
