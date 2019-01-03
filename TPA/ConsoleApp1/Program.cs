using Serialization;
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
        public static void Main(String[] args)
        {
            ViewModelClass viewModel = new ViewModelClass(new BrowseConsole());
            viewModel.Browse();

            Ser ser = new Ser(viewModel.pathVariable);
            int childId = 0;
            ConsoleKeyInfo c;
            while (true)
            {
                //Console.Clear();
                if (viewModel.tree != null)
                PrintTree(viewModel.tree, childId);
                c = Console.ReadKey();
                switch (c.Key)
                {
                    case ConsoleKey.S:
                        ser.serialize();
                        break;
                    case ConsoleKey.D:
                           viewModel.tree = new ModelTreeHandler(ser.deserialize());
                        break;
                    case ConsoleKey.R:
                            viewModel.Load();
                        break;
                    case ConsoleKey.C:
                        if (viewModel.tree != null)
                        {
                            viewModel.tree.Close();
                            childId = 0;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (childId > 0) childId--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (viewModel.tree != null)
                            if (childId < viewModel.tree.currentNode.Nodes.Count - 1) childId++;
                        break;
                    case ConsoleKey.Spacebar:
                        if (viewModel.tree != null)
                        if (viewModel.tree.currentNode.Nodes.Count > childId)
                        {
                            viewModel.tree.GoToChild(viewModel.tree.currentNode.Nodes[childId]);
                            viewModel.tree.Load();
                            childId = 0;
                        }
                        
                        break;
                }
            }
        }

        private static void PrintTree(ModelTreeHandler tree, int childId)
        {
            Console.Clear();
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
