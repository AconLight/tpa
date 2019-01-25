using ViewModel.ModelTree;
using System.Reflection;
using ViewModel.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using Composition;
using ModelTransfer;
using Reflection.Model;
using System.Diagnostics;
using System.Threading;

namespace ViewModel.viewmodel
{
    public class ViewModelClass : INotifyPropertyChanged
    {
        public ObservableCollection<ViewModelNode> HierarchicalAreas { get; set; }
        public MEF composition;
        public ViewModelNode root { get; set; }
        public ModelNode modelRoot { get; set; }
        public AssemblyMetaData assemblyMetaData { get; set; }
        public Assembly assembly { get; set; }
        public string pathVariable { get; set; }
        public RelayCommand Click_Load { get; }
        public RelayCommand Click_Browse { get; }
        public RelayCommand Click_Serialize { get; }
        public RelayCommand Click_Deserialize { get; }
        public IBrowse browse;

        public ViewModelClass(IBrowse browse)
        {
            
            HierarchicalAreas = new ObservableCollection<ViewModelNode>();
            Click_Browse = new RelayCommand(Browse);
            Click_Load = new RelayCommand(Load);
            Click_Serialize = new RelayCommand(serialize);
            Click_Deserialize = new RelayCommand(deserialize);
            pathVariable = "Choose file";
            composition = new MEF();
            this.browse = browse;
            composition.ComposeApp();
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
                assemblyMetaData = new AssemblyMetaData(Assembly.ReflectionOnlyLoadFrom(pathVariable));
                modelRoot = ModelTreeGenerator.Generate(assemblyMetaData as ModelNodePrototype);
                root = new ViewModelNode(modelRoot);
                root.OnCreate();
                root.Load();
                HierarchicalAreas.Add(root);
                Debug.WriteLine("siema");
                Debug.WriteLine(root.MyNodes.Count);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
        }
       public void serialize()
       {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                assemblyMetaData.Save();
            }).Start();
        }
        public void deserialize()
        {
            new Thread(() =>
            {
                modelRoot = new ModelNode(null, assemblyMetaData.Load());
                root = new ViewModelNode(modelRoot);
                root.OnCreate();
                root.Load();
                HierarchicalAreas.Clear();
                HierarchicalAreas.Add(root);
                Debug.WriteLine("deserialisdasdas");
                Debug.WriteLine(root.MyNodes[0].Name);
            }).Start();
        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            MEF.getContainer().Dispose();
        }

    }

}
