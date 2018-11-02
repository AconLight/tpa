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

        public ModelTreeHandler(AssemblyMetaData assemblyMetaData)
        {
            currentNode = new ModelNodeAssembly(null, assemblyMetaData);
            currentNode.load();
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
                currentNode.load();
            }

        }

        public List<ModelNode> getChildren()
        {
            return currentNode.nodes;
        }
    }

}
