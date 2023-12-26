using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectList.Models
{
    public class Task : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _description;
        private bool _completed;
        public int Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        public string Description { get { return _description;} set { _description = value; OnPropertyChanged("Description"); } }
        public bool Completed { get { return _completed; } set { _completed = value; OnPropertyChanged("Completed"); } }
        public Task(int id, string name, string description) 
        {
            Id = id;
            Name = name;
            Description= description;
            Completed = false;
        }

        public Command RemoveCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    try
                    {
                        ((ObservableCollection<Task>)((DataGrid)obj).ItemsSource).Remove(this);
                    }
                    catch { }
                });
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
