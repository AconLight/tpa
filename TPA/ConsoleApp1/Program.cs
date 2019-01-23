using System;
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
            

            //Ser ser = new Ser(viewModel.pathVariable);
            
            int childId = 0;
            ConsoleKeyInfo c;
            while (true)
            {

                c = Console.ReadKey();
                switch (c.Key)
                {
                    case ConsoleKey.S:
                        viewModel.serialize();
                        break;
                    case ConsoleKey.D:
                        viewModel.deserialize();
                        break;
                    case ConsoleKey.R:
                            viewModel.Load();
                        break;
                    case ConsoleKey.C:

                    case ConsoleKey.UpArrow:

                    case ConsoleKey.DownArrow:

                    case ConsoleKey.Spacebar:
 
                        break;
                }
            }
        }

        private static void PrintTree(int tree, int childId)
        {
            //Console.Clear();
            int tabs = 0;
            Console.WriteLine();
            Console.WriteLine("Tree Print:");
            //NodePrint(tabs, tree.rootNode, tree.currentNode, childId);
        }

        private static void NodePrint(int tabs, ViewModelNode node, ViewModelNode currentNode, int childId)
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
            //foreach (ViewModelNode n in ViewModelTreeHandler.TreePrint(node))
            //{
               // NodePrint(tabs, n, currentNode, childId);
            //}
        }

        
    }
}
