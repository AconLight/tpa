using Database.model;
using ModelTransfer;
using Reflection;
using Reflection.Model;
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
        public AssemblyMetaData read()
        {
            /*Console.WriteLine("start");
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
            }*/
            return null;
        }

        public void write(AssemblyMetaData root)
        {
            /*Console.WriteLine("siema write");
            using (var db = new ModelContext())
            {
                DBModelNode root = createDBModel(tree);
                Console.WriteLine("created db model");
                db.nodes.Add(root);
                Console.WriteLine("added root");
                db.SaveChanges();
                Console.WriteLine("saved to db");
            }*/
        }

        public void save(ModelNodePrototype assembly)
        {
            throw new NotImplementedException();
        }

        public ModelNodePrototype load()
        {
            throw new NotImplementedException();
        }
    }
}
