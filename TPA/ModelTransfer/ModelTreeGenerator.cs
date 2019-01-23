using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            root.Protoype.OnCreate();
            List<ModelNodePrototype> loadedNodes = new List<ModelNodePrototype>();
            //create all prototypes
            TreeSeek(node, loadedNodes);
            return root;
        }

        private static void TreeSeek(ModelNodePrototype prot, List<ModelNodePrototype> loadedNodes)
        {
            prot.OnLoad();
            loadedNodes.Add(prot);
            bool flaga = false;
            foreach (ModelNodePrototype p in prot.Nodes)
            {
                p.OnCreate();
                p.Nodes = new List<ModelNodePrototype>();
                flaga = false;
                foreach (ModelNodePrototype p2 in loadedNodes)
                {
                    if (p.Name == p2.Name && p.TypeName == p2.TypeName)
                    {
                        flaga = true;
                        foreach (ModelNodePrototype p3 in p2.Nodes)
                        {
                            p.Nodes.Add(p3);
                        }
                    }
                }
                if (!flaga)
                {
                    TreeSeek(p, loadedNodes);
                }
            }
        }
    }
}
