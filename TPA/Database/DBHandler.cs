using Database.model;
using Reflection;
using Reflection.ModelTree;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    [Export("DataBaseSer",typeof(DataBridgeInterface))]
    class DBHandler : DataBridgeInterface
    {
        public LogicModelTreeHandler read()
        {
            Console.WriteLine("start");
            using (var db = new ModelContext())
            {
                List<DBModelNode> dbNodes = new List<DBModelNode>();
                
                var nodes = from b in db.nodes
                            orderby b.Name
                            select b;
                Console.WriteLine("fetched data");
                foreach (var item in nodes)
                {
                    dbNodes.Add(item);
                }
                Console.WriteLine("items added");

                return createLogicModel(dbNodes);
            }
        }

        public void write(LogicModelTreeHandler tree)
        {
            Console.WriteLine("siema write");
            using (var db = new ModelContext())
            {
                DBModelNode root = createDBModel(tree);
                Console.WriteLine("created db model");
                db.nodes.Add(root);
                Console.WriteLine("added root");
                db.SaveChanges();
                Console.WriteLine("saved to db");
            }
        }

        private static DBModelNode cDBModelNode(string TypeName, string Name, DBModelNode Parent)
        {
            DBModelNode n = new DBModelNode();
            n.isLooped = false;
            n.TypeName = TypeName;
            n.Name = Name;
            n.Parent = Parent;
            n.children = new List<DBModelNode>();
            return n;
        }

        private static LogicModelNode cLogicModelNode(DBModelNode node, LogicModelNode parent)
        {
            if (node.TypeName.Contains("Method"))
            {
                return new LogicModelNodeMethod(parent, node.Name);
            }
            else if (node.TypeName.Contains("Namespace"))
            {
                return new LogicModelNodeNamespace(parent, node.Name);
            }
            else if (node.TypeName.Contains("Type") || node.TypeName.Contains("Property")
                || node.TypeName.Contains("Interface") || node.TypeName.Contains("Nested Type")
                || node.TypeName.Contains("Base Type") || node.TypeName.Contains("Declaring Type")
                || node.TypeName.Contains("Return Type") || node.TypeName.Contains("Parameter Type"))
            {
                return new LogicModelNodeType(parent, node.Name, node.TypeName);
            }
            return null;
        }


        public static DBModelNode createDBModel(LogicModelTreeHandler tree)
        {

            DBModelNode dbRoot = cDBModelNode(tree.rootNode.TypeName, tree.rootNode.Name, null);
            List<DBModelNode> loadedNodes = new List<DBModelNode>();
            TreeSeekToDB(dbRoot, tree.rootNode, loadedNodes);

            return dbRoot;
        }
        private static int myId = 0;
        private static void TreeSeekToDB(DBModelNode myNode, LogicModelNode node, List<DBModelNode> loadedNodes)
        {
            DBModelNode newNode = null;
            loadedNodes.Add(myNode);
            foreach (LogicModelNode child in node.allNodes)
            {
                myNode.children.Add(newNode = cDBModelNode(child.TypeName, child.Name, myNode));
                newNode.Parent = myNode;
                TreeSeekToDB(newNode, child, loadedNodes);

                foreach (DBModelNode vn in loadedNodes)
                {
                    if (vn.TypeName == newNode.TypeName && vn.Name == newNode.Name)
                    {
                        newNode.isLooped = true;
                        break;
                    }
                }
                if (!newNode.isLooped)
                    TreeSeekToDB(newNode, child, loadedNodes);
            }


        }

        public static LogicModelTreeHandler createLogicModel(List<DBModelNode> nodes)
        {
            LogicModelTreeHandler modelTree = new LogicModelTreeHandler();
            LogicModelNodeAssembly root = null;
            DBModelNode dbRoot = null;
            foreach (DBModelNode node in nodes)
            {
                if (node.Parent == null)
                {
                    dbRoot = node;
                    root = new LogicModelNodeAssembly(null, node.Name);
                    break;
                }
            }
            modelTree.rootNode = root;
            modelTree.currentNode = root;

            List<LogicModelNode> loadedNodes = new List<LogicModelNode>();

            TreeSeekToLogic(root, dbRoot, loadedNodes);

            return modelTree;
        }

        private static void TreeSeekToLogic(LogicModelNode myNode, DBModelNode node, List<LogicModelNode> loadedNodes)
        {
            LogicModelNode newNode = null;
            foreach (DBModelNode child in node.children)
            {
                if (child.TypeName.Contains("Method"))
                {
                    myNode.allNodes.Add(newNode = new LogicModelNodeMethod(myNode, child.Name));
                }
                else if (child.TypeName.Contains("Namespace"))
                {
                    myNode.allNodes.Add(newNode = new LogicModelNodeNamespace(myNode, child.Name));
                }
                else if (child.TypeName.Contains("Type") || child.TypeName.Contains("Property")
                    || child.TypeName.Contains("Interface") || child.TypeName.Contains("Nested Type")
                    || child.TypeName.Contains("Base Type") || child.TypeName.Contains("Declaring Type")
                    || child.TypeName.Contains("Return Type") || child.TypeName.Contains("Parameter Type"))
                {
                    myNode.allNodes.Add(newNode = new LogicModelNodeType(myNode, child.Name, child.TypeName));
                }
                else
                {
                    return;
                }
                

                foreach (LogicModelNode vn in loadedNodes)
                {
                    if (vn.TypeName == newNode.TypeName && vn.Name == newNode.Name)
                    {
                        newNode.isLooped = true;
                        break;
                    }
                }
                if(!newNode.isLooped)
                    TreeSeekToLogic(newNode, child, loadedNodes);
            }
            myNode.loadNodes();
        }




    }
}
