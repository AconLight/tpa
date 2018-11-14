using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ModelTree
{
    public class ModelNode
    {
        public string TypeName;
        public ModelNode Parent;
        public string Name { get; set; }
        public ObservableCollection<ModelNode> Nodes { get; set; }
        public Boolean IsExpanded { get; set; }

        public bool IsExpanded2
        {
            get { return IsExpanded; }
            set
            {
                if (value) Load();
                else Close();
            }
        }

        public ModelNode(ModelNode Parent)
        {
            IsExpanded = false;
            this.Parent = Parent;
            Nodes = new ObservableCollection<ModelNode>();
        }

        public virtual void Load()
        {
            // do nothing
        }

        public void Close()
        {
            IsExpanded = false;
            foreach (ModelNode node in Nodes)
            {
                node.Close();
            }
            Nodes.Clear();
        }
    }
}
