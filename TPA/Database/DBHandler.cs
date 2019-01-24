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

        public void save(AssemblyMetaData assembly)
        {
            using (var db = new ModelContext())
            {
                DBModelNode root = new DBModelNode();
                root.Name = assembly.Name;
                root.TypeName = assembly.TypeName;
                root.Mods = assembly.Mods;
                List<DBModelNode> loaded = new List<DBModelNode>();
                loaded.Add(root);
                root.create(assembly, loaded);
                Console.WriteLine("created db model");
                db.nodes.Add(root);
                Console.WriteLine("added root");
                db.SaveChanges();
                Console.WriteLine("saved to db");
            }
        }

        public ModelNodePrototype load()
        {
            using (var db = new ModelContext())
            {
                List<DBModelNode> dbNodes = new List<DBModelNode>();
                
                var nodes = from b in db.nodes
                            orderby b.Name
                            select b;
                Console.WriteLine("fetched data");
                DBModelNode root = null;
                foreach (DBModelNode item in nodes)
                {
                    if(item.TypeName == "Assembly")
                    {
                        root = item;
                        break;
                    }
                }
                Console.WriteLine("items added");

                ModelNodePrototype prot = new ModelNodePrototype();
                prot.Name = root.Name;
                prot.TypeName = root.TypeName;
                prot.Mods = root.Mods;
                List<ModelNodePrototype> loaded = new List<ModelNodePrototype>();
                loaded.Add(prot);
                root.retrievePrototype(prot, loaded);
                return prot;
            }
        }
    }
}
