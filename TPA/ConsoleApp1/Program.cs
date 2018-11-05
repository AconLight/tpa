using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Reflection.Model;
using Reflection.ModelTree;

namespace ConsoleApp1
{
    class Program
    {
        static readonly string assemblyFile = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"TPA.ApplicationArchitecture.dll");

        public static void Main(String[] args)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            AssemblyMetaData assemblyMetadata = new AssemblyMetaData(assembly);
            ModelTreeHandler tree = new ModelTreeHandler(assemblyMetadata);

            int childIterator;
            int childId = 0;
            ConsoleKeyInfo c;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("current node:");
                Console.WriteLine(tree.currentNode.Name);
                Console.WriteLine("current node children:");
                childIterator = 0;
                foreach (ModelNode child in tree.currentNode.nodes)
                {
                    Console.WriteLine(childIterator + ") " + child.Name);
                    childIterator++;
                }
                Console.WriteLine("chosen node children id: " + childId);

                c = Console.ReadKey();
                switch (c.Key)
                {
                    case ConsoleKey.P:
                        tree.GoToParent();
                        break;
                    case ConsoleKey.DownArrow:
                        if (childId > 0) childId--;
                        break;
                    case ConsoleKey.UpArrow:
                        if (childId < tree.currentNode.nodes.Count - 1) childId++;
                        break;
                    case ConsoleKey.Spacebar:
                        if (tree.currentNode.nodes.Count > childId) tree.GoToChild(tree.currentNode.nodes[childId]);
                        childId = 0;
                        break;
                    case ConsoleKey.Escape:
                        //
                        break;

                }
            }
        }
    }
}
