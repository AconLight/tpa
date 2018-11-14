using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.ModelTree
{
    public class ModelNode
    {
        public string TypeName;
        public string Name;
        public ModelNode parent;
        public List<ModelNode> nodes;
        public Boolean isOpen;

        public ModelNode(ModelNode parent)
        {
            isOpen = false;
            this.parent = parent;
            nodes = new List<ModelNode>();
        }

        public virtual void Load()
        {
            // do nothing
        }

        public void Close()
        {
            isOpen = false;
            foreach (ModelNode node in nodes)
            {
                node.Close();
            }
            nodes.Clear();
        }
    }
}
