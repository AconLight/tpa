using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ModelTree
{
    public class ViewModelNode
    {
        public bool isLooped = false;
        public ObservableCollection<ViewModelNode> allNodes { get; set; }
        public string TypeName;
        public ViewModelNode Parent;
        public string Name { get; set; }
        public ObservableCollection<ViewModelNode> Nodes { get; set; }
        public Boolean IsExpanded { get; set; }

        public bool OpenClose
        {
            get { return IsExpanded; }
            set
            {
                if (value) Load();
                else Close();
            }
        }

        public ViewModelNode(ViewModelNode Parent)
        {
            IsExpanded = false;
            this.Parent = Parent;
            Nodes = new ObservableCollection<ViewModelNode>();
            allNodes = new ObservableCollection<ViewModelNode>();
        }

        public virtual void Load()
        {
            // do nothing
        }

        public void Close()
        {
            IsExpanded = false;
            foreach (ViewModelNode node in Nodes)
            {
                node.Close();
            }
            //Nodes.Clear();
        }
        public virtual void loadAll()
        {
            // do nothing
        }
    }
}
