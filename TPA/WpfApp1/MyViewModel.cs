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
        public MyViewModel()
        {
            
            Click_Button = new RelayCommand(LoadDLL);
            Click_Browse = new RelayCommand(Browse);
        }

        #region DataContext
        Assembly assembly;
        AssemblyMetaData assemblyMetadata;
        ModelTreeHandler tree;
        public string PathVariable { get; set; }
        public Visibility ChangeControlVisibility { get; set; } = Visibility.Hidden;
        public ICommand Click_Browse { get; }
        public ICommand Click_Button { get; }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
        }
        #endregion

        #region private
        private void LoadDLL()
        {
            if (PathVariable.Substring(PathVariable.Length - 4) == ".dll")
                TreeViewLoaded();
        }
        private void TreeViewLoaded()
        {
            assembly = Assembly.LoadFrom(PathVariable);
            assemblyMetadata = new AssemblyMetaData(assembly);
            tree = new ModelTreeHandler(assemblyMetadata);
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
                PathVariable = test.FileName;
                ChangeControlVisibility = Visibility.Visible;
                RaisePropertyChanged("ChangeControlVisibility");
                RaisePropertyChanged("PathVariable");
            }
        }
        #endregion
    }
}
