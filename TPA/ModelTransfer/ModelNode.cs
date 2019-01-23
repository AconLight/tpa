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
        public void Create()
        {
            this.OnCreate();
        }

        protected virtual void OnCreate()
        {
            //rewrite creation data
        }
        public void Load()
        {
            Protoype.OnLoad();
            Nodes.Clear();
            foreach(ModelNodePrototype prot in Protoype.Nodes)
            {
                Nodes.Add(new ModelNode(this, prot));
            }
            this.OnLoad();
        }

        protected virtual void OnLoad()
        {
            //rewrite loading data
        }


    }
}
