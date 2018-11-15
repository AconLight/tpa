using ViewModel.ModelTree;
using System.Reflection;
using ViewModel.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ViewModel.viewmodel
{
    public class ViewModelClass
    {
        public RelayCommand Click_Browse { get; }
        public ObservableCollection<ModelNode> HierarchicalAreas { get; set; }
        public RelayCommand Click_Load { get; }
        public ModelNode root { get; set; }
        Assembly assembly { get; set; }
        public string pathVariable { get; set; }

        private ModelTreeHandler tree;

        public ViewModelClass()
        {
            Click_Browse = new RelayCommand(Browse);
            Click_Load = new RelayCommand(Load);
            HierarchicalAreas = new ObservableCollection<ModelNode>();
            pathVariable = "Choose file";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
        }
        public virtual void Browse()
        {
        }
        private void Load()
        {
            if (pathVariable.Substring(pathVariable.Length - 4) == ".dll")
            {
                tree = new ModelTreeHandler(Assembly.LoadFrom(pathVariable));
                tree.Load();
                root = tree.rootNode;
                HierarchicalAreas.Add(root);
                RaisePropertyChanged(nameof(HierarchicalAreas));
            }
        }

    }
}
