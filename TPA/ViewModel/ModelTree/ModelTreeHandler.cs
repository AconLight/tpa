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
        public ModelNodeAssembly rootNode;

        public ModelTreeHandler(Assembly assembly)
        {
            rootNode = new ModelNodeAssembly(null, new AssemblyMetaData(assembly));
            currentNode = rootNode;
            //Load();
        }
        public ModelTreeHandler(AssemblyMetaData assemblyMetaData)
        {
            rootNode = new ModelNodeAssembly(null, assemblyMetaData);
            currentNode = rootNode;
            //Load();
        }
        public ModelTreeHandler(Reflection.ModelTree.ModelTreeHandler tree)
        {
            rootNode = new ModelNodeAssembly(null, tree.rootNode.Name);
            tree.rootNode.loadAll();
            Console.WriteLine(tree.rootNode.Nodes.Count());
            currentNode = rootNode;
            TreeSeek(rootNode, tree.rootNode);
            Console.WriteLine(rootNode.Nodes.Count());
            Load();

        }

        private void TreeSeek(ModelNode myNode, Reflection.ModelTree.ModelNode node)
        {
            ModelNode newNode = null;
            node.loadAll();
            foreach (Reflection.ModelTree.ModelNode child in node.allNodes)
            {
                if (child.TypeName == "Method")
                {
                    myNode.Nodes.Add(newNode = new ModelNodeMethod(myNode, ((Reflection.ModelTree.ModelNodeMethod)child).Name));
                }
                else if (child.TypeName == "Namespace")
                {
                    myNode.Nodes.Add(newNode = new ModelNodeNamespace(myNode, ((Reflection.ModelTree.ModelNodeNamespace)child).Name));
                }
                else if (child.TypeName == "Type" || child.TypeName == "Property" 
                    || child.TypeName == "Interface" || child.TypeName == "Nested Type" 
                    || child.TypeName == "Base Type" || child.TypeName == "Declaring Type"
                    || child.TypeName == "Return Type" || child.TypeName == "Parameter Type")
                {
                    myNode.Nodes.Add(newNode = new ModelNodeType(myNode, ((Reflection.ModelTree.ModelNodeType)child).Name, ((Reflection.ModelTree.ModelNodeType)child).TypeName));
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

        public static Reflection.ModelTree.ModelTreeHandler createModelTree(ModelTreeHandler tree)
        {
            Reflection.ModelTree.ModelTreeHandler modelTree = new Reflection.ModelTree.ModelTreeHandler();

            modelTree.rootNode = new Reflection.ModelTree.ModelNodeAssembly(null, tree.rootNode.Name);
            tree.rootNode.loadAll();
            modelTree.currentNode = modelTree.rootNode;
            TreeSeek(modelTree.rootNode, tree.rootNode);
            modelTree.Load();
            return modelTree;
        }

        private static void TreeSeek(Reflection.ModelTree.ModelNode myNode, ModelNode node)
        {
            Reflection.ModelTree.ModelNode newNode = null;
            node.loadAll();
            foreach (ModelNode child in node.Nodes)
            {
                if (child.TypeName == "Method")
                {
                    myNode.allNodes.Add(newNode = new Reflection.ModelTree.ModelNodeMethod(myNode, ((ModelNodeMethod)child).Name));
                }
                else if (child.TypeName == "Namespace")
                {
                    myNode.allNodes.Add(newNode = new Reflection.ModelTree.ModelNodeNamespace(myNode, ((ModelNodeNamespace)child).Name));
                }
                else if (child.TypeName == "Type" || child.TypeName == "Property"
                    || child.TypeName == "Interface" || child.TypeName == "Nested Type"
                    || child.TypeName == "Base Type" || child.TypeName == "Declaring Type"
                    || child.TypeName == "Return Type" || child.TypeName == "Parameter Type")
                {
                    myNode.allNodes.Add(newNode = new Reflection.ModelTree.ModelNodeType(myNode, ((ModelNodeType)child).Name, ((ModelNodeType)child).TypeName));
                }
                else
                {
                    return;
                }

                TreeSeek(newNode, child);
            }
        }
    }

}
