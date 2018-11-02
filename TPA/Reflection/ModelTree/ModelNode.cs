using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.ModelTree
{
    public class ModelNode
    {
        public string Name;
        public ModelNode parent;
        public List<ModelNode> nodes;

        public ModelNode(ModelNode parent)
        {
            this.parent = parent;
            nodes = new List<ModelNode>();
        }

        public virtual void load()
        {
            Console.WriteLine("Not Implemented!");
        }
    }
}
