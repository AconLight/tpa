using Microsoft.Win32;
using Reflection.Model;
using Reflection.ModelTree;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        public RelayCommand Click_Load { get; }
        public string pathVariable { get; set; }
        private Assembly assembly { get; set; }
        private AssemblyMetaData assemblyMetadata { get; set; }
        private ModelTreeHandler tree { get; set; }

        public MyViewModel()
        {
            Click_Browse = new RelayCommand(Browse);
            Click_Load = new RelayCommand(Load);
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            pathVariable = "Choose .dll file to be loaded.";
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
                RaisePropertyChanged(nameof(pathVariable));
            }
        }
        private void Load()
        {
            if (pathVariable.Substring(pathVariable.Length - 4) == ".dll")
            {
                assembly = Assembly.LoadFrom(pathVariable);
                assemblyMetadata = new AssemblyMetaData(assembly);
                tree = new ModelTreeHandler(assemblyMetadata);
                TreeViewItem root = new TreeViewItem(tree.currentNode, "(" + tree.currentNode.TypeName + ") " + tree.currentNode.Name);   
                HierarchicalAreas.Add(root);
                RaisePropertyChanged(nameof(HierarchicalAreas));
                return;
            }
            else
            {
                MessageBox.Show("You choose wrong file type, havent choose at all." ,"Warning",MessageBoxButton.OK ,MessageBoxImage.Warning);
            }


        }
    }
}
