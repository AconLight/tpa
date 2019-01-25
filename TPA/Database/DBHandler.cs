using Database.model;
using ModelTransfer;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Configuration;

namespace Database
{
    [Export("DataBaseSer",typeof(DataBridgeInterface))]
    class DBHandler : DataBridgeInterface
    {
        public void save(ModelNodePrototype assembly)
        {
            using (var db = new ModelContext(ConfigurationManager.AppSettings["DefaultConnection"]))
            {
                //Debug.WriteLine(ConfigurationManager.ConnectionStrings["Database.Properties.Settings.ConnectionString"].ConnectionString);
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
        static DBModelNode temp = null;

        public ModelNodePrototype load()
        {
            using (var db = new ModelContext(ConfigurationManager.AppSettings["DefaultConnection"]))
            {
                List<DBModelNode> dbNodes = new List<DBModelNode>();
                
                var nodes = from b in db.nodes
                            orderby b.Name
                            select b;
                Console.WriteLine("fetched data");
                DBModelNode root = null;
                List<DBModelNode> looped = new List<DBModelNode>();
                List<DBModelNode> looped2 = new List<DBModelNode>();
                foreach (DBModelNode item in nodes)
                {
                    if (item.isLooped)
                    {
                        DBModelNode n = new DBModelNode();
                        n.Name = item.Name;
                        n.TypeName = item.TypeName;
                        looped.Add(item);
                    }
                        
                }

                foreach (DBModelNode item2 in nodes)
                {
                    foreach (DBModelNode item in looped)
                    {

                        if (!item2.isLooped && item.Name == item2.Name && item.TypeName == item2.TypeName)
                        {
                            looped2.Add(item2);
                        }
                    }
                }

                foreach (DBModelNode item in looped)
                {
                    foreach (DBModelNode item2 in looped2)
                    {
                        if (item.Name == item2.Name && item.TypeName == item2.TypeName)
                        {
                            item.children = new List<DBModelNode>();
                            foreach (DBModelNode item3 in item2.children)
                            {
                                item.children.Add(item3);
                            }
                        }

                    }
                }

                    foreach (DBModelNode item in nodes)
                {
                    if(item.TypeName == "Assembly")
                    {
                        root = item;
                    }
                    if (item.isLooped)
                    {
                        foreach (DBModelNode item2 in looped)
                        {
                            if (!item2.isLooped && item.Name == item2.Name && item.TypeName == item2.TypeName)
                            {
                                item.Parent = item2.Parent;
                                item.children = item2.children;
                                break;
                            }
                        }
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
