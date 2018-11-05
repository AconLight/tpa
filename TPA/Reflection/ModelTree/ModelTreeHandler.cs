using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reflection.Model;

namespace Reflection.ModelTree
{
    public class ModelTreeHandler
    {
        public ModelNode currentNode;
        public ModelNode rootNode;

        public ModelTreeHandler(AssemblyMetaData assemblyMetaData)
        {
            currentNode = new ModelNodeAssembly(null, assemblyMetaData);
            rootNode = currentNode;
            Load();
        }

        public void GoToParent()
        {
            if (currentNode.parent != null) currentNode = currentNode.parent;
        }

        public void GoToChild(ModelNode node)
        {
            if (node != null)
            {
                currentNode = node;
            }
        }

        public void Load()
        {
            currentNode.Load();
        }

        public void Close()
        {
            currentNode.Close();
        }

        public List<ModelNode> getChildren()
        {
            return currentNode.nodes;
        }

        public static List<ModelNode> TreePrint(ModelNode node)
        {
            List<ModelNode> nodes = new List<ModelNode>();
            foreach (ModelNode n in node.nodes)
            {
                if (n.isOpen)
                {
                    nodes.Add(n);
                }
            }

            return nodes;
        }
    }

}
