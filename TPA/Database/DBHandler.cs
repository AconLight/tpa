using Database.model;
using Reflection;
using Reflection.ModelTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class DBHandler : DataBridgeInterface
    {
        public LogicModelTreeHandler read()
        {
            return null;
        }

        public void write(LogicModelTreeHandler tree)
        {
            using (var db = new ModelContext())
            {
                List<DBModelNode> nodes = createDBModel(tree);
                foreach (DBModelNode node in nodes)
                {
                    db.nodes.Add(node);
                }
                db.SaveChanges();
            }
        }

        private static DBModelNode cDBModelNode(string TypeName, string Name, DBModelNode Parent)
        {
            DBModelNode n = new DBModelNode();
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


        public static List<DBModelNode> createDBModel(LogicModelTreeHandler tree)
        {

            DBModelNode dbRoot = cDBModelNode(tree.rootNode.TypeName, tree.rootNode.Name, null);
            List<DBModelNode> loadedNodes = new List<DBModelNode>();

            TreeSeekToDB(dbRoot, tree.rootNode, loadedNodes);

            return loadedNodes;
        }

        private static void TreeSeekToDB(DBModelNode myNode, LogicModelNode node, List<DBModelNode> loadedNodes)
        {
            if (node.isLooped) return;

            DBModelNode newNode = null;
            foreach (LogicModelNode child in node.allNodes)
            {
                myNode.children.Add(newNode = cDBModelNode(node.TypeName, node.Name, myNode));
                loadedNodes.Add(myNode);
                TreeSeekToDB(newNode, child, loadedNodes);
            }
        }




    }
}
