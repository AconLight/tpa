using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTransfer
{
    public class ModelNode
    {
        public ModelNodePrototype Protoype;
        public List<ModelNode> Nodes;
        public ModelNode Parent;
        public ModelNode(ModelNode Parent, ModelNodePrototype Protoype)
        {
            this.Parent = Parent;
            this.Protoype = Protoype;
            Nodes = new List<ModelNode>();
        }

        public virtual void OnCreate()
        {
            //rewrite creation data
        }

        public virtual void OnLoad()
        {
            //rewrite loading data
        }

        public void Load()
        {
            LoadNodesFromProt();
            OnLoad();
        }

        public void LoadNodesFromProt()
        {
            Nodes.Clear();
            foreach (ModelNodePrototype p in Protoype.Nodes)
            {
                Nodes.Add(new ModelNode(this, p));
            }
        }


    }
}
