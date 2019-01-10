using ViewModel.ModelTree;
using System.Reflection;
using ViewModel.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Reflection.ModelTree;
using System;

namespace ViewModel.viewmodel
{
    public class ViewModelClass : INotifyPropertyChanged
    {
        public ObservableCollection<ViewModelNode> HierarchicalAreas { get; set; }
        public ViewModelNode root { get; set; }
        public Assembly assembly { get; set; }
        public string pathVariable { get; set; }
        public ViewModelTreeHandler tree;
        public LogicModelTreeHandler logicTree;
        public RelayCommand Click_Load { get; }
        public RelayCommand Click_Browse { get; }
        public IBrowse browse;

        public ViewModelClass(IBrowse browse)
        {
            HierarchicalAreas = new ObservableCollection<ViewModelNode>();
            Click_Browse = new RelayCommand(Browse);
            Click_Load = new RelayCommand(Load);
            pathVariable = "Choose file";
            this.browse = browse;
        }
        public void Browse()
        {
            pathVariable = browse.Browse();
            RaisePropertyChanged("pathVariable");
        }
            
        public void Load()
        {
            
            if (pathVariable.Substring(pathVariable.Length - 4) == ".dll")
            {
                logicTree = new LogicModelTreeHandler(Assembly.LoadFrom(pathVariable));
                logicTree.Load();
                Console.WriteLine("logic tree: " + logicTree.rootNode.allNodes.Count);
                Console.WriteLine("load viewmodel");
                tree = new ViewModelTreeHandler(logicTree);
                Console.WriteLine("load viewmodel finished");
                root = tree.rootNode;
                HierarchicalAreas.Add(root);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
        }

    }

}
