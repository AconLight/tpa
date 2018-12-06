using ViewModel.ModelTree;
using System.Reflection;
using ViewModel.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ViewModel.viewmodel
{
    public class ViewModelClass : INotifyPropertyChanged
    {
        public ObservableCollection<ModelNode> HierarchicalAreas { get; set; }
        public ModelNode root { get; set; }
        public Assembly assembly { get; set; }
        public string pathVariable { get; set; }
        public ModelTreeHandler tree;
        public RelayCommand Click_Load { get; }
        public RelayCommand Click_Browse { get; }
        public IBrowse browse;

        public ViewModelClass(IBrowse browse)
        {
            HierarchicalAreas = new ObservableCollection<ModelNode>();
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
                tree = new ModelTreeHandler(Assembly.LoadFrom(pathVariable));
                tree.Load();
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
