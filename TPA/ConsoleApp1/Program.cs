using System;
using System.IO;
using System.Linq;
using System.Reflection;
using ViewModel.ModelTree;
using ViewModel.viewmodel;

namespace ConsoleApp1
{
    class Program
    {
        static readonly string assemblyFile = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"TPA.ApplicationArchitecture.dll");

        public static void Main(String[] args)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            ViewModelClass viewModel = new ViewModelClass();
            viewModel.LoadTree(assembly);

            ModelTreeHandler tree = viewModel.Tree;


            tree.Load();
            int childIterator;
            int childId = 0;
            ConsoleKeyInfo c;
            while (true)
            {
                Console.Clear();
                /*Console.WriteLine();
                Console.WriteLine("current node:");
                Console.WriteLine("(" + tree.currentNode.TypeName + ") " + tree.currentNode.Name);
                Console.WriteLine("current node children:");
                childIterator = 0;
                foreach (ModelNode child in tree.currentNode.Nodes)
                {
                    Console.WriteLine(childIterator + ") " + "(" + child.TypeName + ") " + child.Name);
                    childIterator++;
                }
                Console.WriteLine("chosen node children id: " + childId);
                Console.WriteLine();*/
                PrintTree(tree, childId);
                c = Console.ReadKey();
                switch (c.Key)
                {
                    case ConsoleKey.P:
                        tree.GoToParent();
                        break;
                    case ConsoleKey.L:
                        tree.Load();
                        break;
                    case ConsoleKey.T:
                        PrintTree(tree, childId);
                        break;
                    case ConsoleKey.C:
                        tree.Close();
                        childId = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        if (childId > 0) childId--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (childId < tree.currentNode.Nodes.Count - 1) childId++;
                        break;
                    case ConsoleKey.Spacebar:
                        if (tree.currentNode.Nodes.Count > childId)
                        {
                            tree.GoToChild(tree.currentNode.Nodes[childId]);
                            tree.Load();
                            childId = 0;
                        }
                        
                        break;
                    case ConsoleKey.Escape:
                        //
                        break;

                }
            }
        }

        private static void PrintTree(ModelTreeHandler tree, int childId)
        {
            int tabs = 0;
            Console.WriteLine();
            Console.WriteLine("Tree Print:");
            NodePrint(tabs, tree.rootNode, tree.currentNode, childId);
        }

        private static void NodePrint(int tabs, ModelNode node, ModelNode currentNode, int childId)
        {
            for(int i = 0; i < tabs; i++)
            {
                Console.Write("   ");
            }
            if (node.Parent == currentNode && childId == node.Parent.Nodes.IndexOf(node))
            {
                Console.Write("-->");
            } else
            {
                Console.Write("   ");
            }
                Console.WriteLine("(" + node.TypeName + ") " + node.Name);
            tabs++;
            foreach (ModelNode n in ModelTreeHandler.TreePrint(node))
            {
                NodePrint(tabs, n, currentNode, childId);
            }
        }

        
    }
}
