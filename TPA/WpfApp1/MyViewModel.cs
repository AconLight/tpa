using Microsoft.Win32;
using Reflection.Model;
using Reflection.ModelTree;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    class MyViewModel : INotifyPropertyChanged
    {
        public RelayCommand Click_Browse { get; }
        public RelayCommand Click_Load { get; }
        Assembly assembly { get; set; }
        public Visibility ChangeControlVisibility { get; set; } = Visibility.Hidden;
        public string pathVariable { get; set; }

        public MyViewModel()
        {
            Click_Browse = new RelayCommand(Browse);
            Click_Load = new RelayCommand(Load);

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
        }
        private void Browse()
        {
            OpenFileDialog test = new OpenFileDialog()
            {
                Filter = "Dynamic Library File(*.dll)| *.dll"
            };
            test.ShowDialog();
            if (test.FileName.Length == 0)
                MessageBox.Show("No files selected");
            else
            {
                pathVariable = test.FileName;
                ChangeControlVisibility = Visibility.Visible;
                RaisePropertyChanged(nameof(ChangeControlVisibility));
                RaisePropertyChanged(nameof(pathVariable));

            }
        }
        private void Load()
        {
            assembly = Assembly.LoadFrom(pathVariable);
        }
    }
}
