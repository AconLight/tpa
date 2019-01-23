using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTransfer
{
    public static class ModelTreeGenerator
    {
        public static ModelNode Generate(ModelNodePrototype node)
        {
            ModelNode root = new ModelNode(null, node);
            //create all prototypes
            TreeSeek(node);
            return root;
        }

        private static void TreeSeek(ModelNodePrototype prot)
        {
            if (prot.Name == null)
            {
                prot.OnCreate();
                prot.OnLoad();
                foreach(ModelNodePrototype p in prot.Nodes)
                {
                    TreeSeek(p);
                }
            }
        }
    }
}
