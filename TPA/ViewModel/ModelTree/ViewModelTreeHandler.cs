using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Reflection.Model;
using Reflection.ModelTree;

namespace ViewModel.ModelTree
{
    public class ViewModelTreeHandler
    {
        public ViewModelNode currentNode;
        public ViewModelNodeAssembly rootNode;

        public ViewModelTreeHandler(Assembly assembly)
        {
            rootNode = new ViewModelNodeAssembly(null, new AssemblyMetaData(assembly));
            currentNode = rootNode;
            //Load();
        }
        public ViewModelTreeHandler(AssemblyMetaData assemblyMetaData)
        {
            rootNode = new ViewModelNodeAssembly(null, assemblyMetaData);
            currentNode = rootNode;
            //Load();
        }
        public ViewModelTreeHandler(LogicModelTreeHandler tree)
        {
            List<ViewModelNode> loadedNodes = new List<ViewModelNode>();
            rootNode = new ViewModelNodeAssembly(null, tree.rootNode.Name);
            Console.WriteLine(tree.rootNode.allNodes.Count());
            //tree.rootNode.loadAll();
            Console.WriteLine(tree.rootNode.Nodes.Count());
            currentNode = rootNode;
            TreeSeek(rootNode, tree.rootNode, loadedNodes);
            Console.WriteLine(rootNode.Nodes.Count());
            Load();

        }

        private void TreeSeek(ViewModelNode myNode, LogicModelNode node, List<ViewModelNode> loadedNodes)
        {
           
            ViewModelNode newNode = null;
            //node.loadAll();
            foreach (Reflection.ModelTree.LogicModelNode child in node.allNodes)
            {
                if (child.TypeName.Contains("Method"))
                {
                    myNode.Nodes.Add(newNode = new ViewModelNodeMethod(myNode, ((Reflection.ModelTree.LogicModelNodeMethod)child).Name));
                }
                else if (child.TypeName.Contains("Namespace"))
                {
                    myNode.Nodes.Add(newNode = new ViewModelNodeNamespace(myNode, ((Reflection.ModelTree.LogicModelNodeNamespace)child).Name));
                }
                else if (child.TypeName.Contains("Type") || child.TypeName.Contains("Property")
                    || child.TypeName.Contains("Interface") || child.TypeName.Contains("Nested Type")
                    || child.TypeName.Contains("Base Type") || child.TypeName.Contains("Declaring Type")
                    || child.TypeName.Contains("Return Type") || child.TypeName.Contains("Parameter Type"))
                {
                    myNode.Nodes.Add(newNode = new ViewModelNodeType(myNode, ((Reflection.ModelTree.LogicModelNodeType)child).Name, ((Reflection.ModelTree.LogicModelNodeType)child).TypeName));
                }
                else
                {
                    return;
                }

                if (node.isLooped)
                {
                    foreach (ViewModelNode vn in loadedNodes)
                    {
                        if (vn.TypeName == node.TypeName && vn.Name == node.Name)
                        {
                            myNode.Nodes.Clear();
                            foreach (ViewModelNode vn2 in vn.Nodes)
                            {
                                myNode.Nodes.Add(vn2);
                            }
                            myNode.isLooped = true;
                            break;
                        }
                    }
                    loadedNodes.Add(myNode);
                    continue;
                }
                loadedNodes.Add(myNode);
                TreeSeek(newNode, child, loadedNodes);
            }
        }

        public void GoToParent()
        {
            if (currentNode.Parent != null) currentNode = currentNode.Parent;
        }

        public void GoToChild(ViewModelNode node)
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

        public ObservableCollection<ViewModelNode> getChildren()
        {
            return currentNode.Nodes;
        }

        public static List<ViewModelNode> TreePrint(ViewModelNode node)
        {
            List<ViewModelNode> nodes = new List<ViewModelNode>();
            foreach (ViewModelNode n in node.Nodes)
            {
                if (n.Parent == null || n.Parent.IsExpanded)
                {
                    nodes.Add(n);
                }
            }

            return nodes;
        }

        public static LogicModelTreeHandler createModelTree(ViewModelTreeHandler tree)
        {
            Console.WriteLine("create reflection model tree from VIEWMODEL");
            LogicModelTreeHandler modelTree = new LogicModelTreeHandler();

            modelTree.rootNode = new LogicModelNodeAssembly(null, tree.rootNode.Name);
            
            tree.rootNode.loadAll();
            modelTree.currentNode = modelTree.rootNode;
            TreeSeek(modelTree.rootNode, tree.rootNode);
            modelTree.Load();
            Console.WriteLine("finished");
            return modelTree;
        }

        private static void TreeSeek(LogicModelNode myNode, ViewModelNode node)
        {
            
            LogicModelNode newNode = null;
            node.loadAll();
            //Console.WriteLine("treeSeek");
            //Console.WriteLine(node.Name + ", childs: " + node.Nodes.Count());
            foreach (ViewModelNode child in node.Nodes)
            {
                if (child.TypeName.Contains("Method"))
                {
                    myNode.allNodes.Add(newNode = new Reflection.ModelTree.LogicModelNodeMethod(myNode, ((ViewModelNodeMethod)child).Name));
                }
                else if (child.TypeName.Contains("Namespace"))
                {
                    myNode.allNodes.Add(newNode = new Reflection.ModelTree.LogicModelNodeNamespace(myNode, ((ViewModelNodeNamespace)child).Name));
                }
                else if (child.TypeName.Contains("Type") || child.TypeName.Contains("Property")
                    || child.TypeName.Contains("Interface") || child.TypeName.Contains("Nested Type")
                    || child.TypeName.Contains("Base Type") || child.TypeName.Contains("Declaring Type")
                    || child.TypeName.Contains("Return Type") || child.TypeName.Contains("Parameter Type"))
                {
                    myNode.allNodes.Add(newNode = new Reflection.ModelTree.LogicModelNodeType(myNode, ((ViewModelNodeType)child).Name, ((ViewModelNodeType)child).TypeName));
                }
                else
                {
                    Console.WriteLine("unhandled: " + child.TypeName);
                    return;
                }
                myNode.loadNodes();
                if (!node.isLooped)
                    TreeSeek(newNode, child);
            }
        }
    }

}
