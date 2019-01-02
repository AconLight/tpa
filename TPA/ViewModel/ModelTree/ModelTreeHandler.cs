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
            Console.WriteLine(tree.rootNode.Nodes.Count());
            rootNode = currentNode;
            TreeSeek(rootNode, tree.rootNode);
            Console.WriteLine(rootNode.Nodes.Count());
            Load();

        }

        private void TreeSeek(ModelNode myNode, Serialization.SerializationModelTree.ModelNode node)
        {
            ModelNode newNode = null;
            node.loadAll();
            foreach (Serialization.SerializationModelTree.ModelNode child in node.allNodes)
            {
                if (child.TypeName == "Method")
                {
                    myNode.Nodes.Add(newNode = new ModelNodeMethod(myNode, ((Serialization.SerializationModelTree.ModelNodeMethod)child).Name));
                }
                else if (child.TypeName == "Namespace")
                {
                    myNode.Nodes.Add(newNode = new ModelNodeNamespace(myNode, ((Serialization.SerializationModelTree.ModelNodeNamespace)child).Name));
                }
                else if (child.TypeName == "Type")
                {
                    myNode.Nodes.Add(newNode = new ModelNodeType(myNode, ((Serialization.SerializationModelTree.ModelNodeType)child).Name, ((Serialization.SerializationModelTree.ModelNodeType)child).TypeName));
                }
                else
                {
                    return;
                }

                TreeSeek(newNode, child);
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
