using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Reflection.Model;

namespace ViewModel.ModelTree
{
    public class ModelTreeHandler
    {
        public ModelNode currentNode;
        public ModelNode rootNode;

        public ModelTreeHandler(Assembly assembly)
        {
            currentNode = new ModelNodeAssembly(null, new AssemblyMetaData(assembly));
            rootNode = currentNode;
            //Load();
        }
        public ModelTreeHandler(Serialization.SerializationModelTree.ModelTreeHandler tree)
        {
            currentNode = new ModelNodeAssembly(null, tree.rootNode.Name);
            tree.rootNode.loadAll();
            foreach (Serialization.SerializationModelTree.ModelNode child in tree.rootNode.allNodes)
            {
                currentNode.Nodes.Add(new ModelNodeNamespace(currentNode, ((Serialization.SerializationModelTree.ModelNodeNamespace)child).m_namespace));
            }
            rootNode = currentNode;
            TreeSeek(rootNode, tree.rootNode);
        }

        private void TreeSeek(ModelNode parent, Serialization.SerializationModelTree.ModelNode node)
        {
            foreach(Serialization.SerializationModelTree.ModelNode child in node.allNodes)
            {
                child.loadAll();
                ModelNode newParent = null;
                if (child.TypeName == "Method")
                {
                    parent.Nodes.Add(newParent = new ModelNodeMethod(parent, ((Serialization.SerializationModelTree.ModelNodeMethod)child).method));
                }
                else if (child.TypeName == "Namespace")
                {
                    parent.Nodes.Add(newParent = new ModelNodeNamespace(parent, ((Serialization.SerializationModelTree.ModelNodeNamespace)child).m_namespace));
                }
                else if (child.TypeName == "Type")
                {
                    parent.Nodes.Add(newParent = new ModelNodeType(parent, ((Serialization.SerializationModelTree.ModelNodeType)child).type, ((Serialization.SerializationModelTree.ModelNodeType)child).TypeName));
                }
                
                TreeSeek(newParent, child);
            }
        }

        public void GoToParent()
        {
            if (currentNode.Parent != null) currentNode = currentNode.Parent;
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
            if (currentNode.Parent != null)
            {
                currentNode.Close();
                currentNode = currentNode.Parent;
            }

        }

        public ObservableCollection<ModelNode> getChildren()
        {
            return currentNode.Nodes;
        }

        public static List<ModelNode> TreePrint(ModelNode node)
        {
            List<ModelNode> nodes = new List<ModelNode>();
            foreach (ModelNode n in node.Nodes)
            {
                if (n.Parent == null || n.Parent.IsExpanded)
                {
                    nodes.Add(n);
                }
            }

            return nodes;
        }
    }

}
