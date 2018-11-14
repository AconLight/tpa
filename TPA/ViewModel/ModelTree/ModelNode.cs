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
        public string Name;
        public ModelNode Parent;
        public ObservableCollection<ModelNode> Nodes;
        public Boolean IsOpen;

        public ModelNode(ModelNode Parent)
        {
            IsOpen = false;
            this.Parent = Parent;
            Nodes = new ObservableCollection<ModelNode>();
        }

        public virtual void Load()
        {
            // do nothing
        }

        public void Close()
        {
            IsOpen = false;
            foreach (ModelNode node in Nodes)
            {
                node.Close();
            }
            Nodes.Clear();
        }
    }
}
