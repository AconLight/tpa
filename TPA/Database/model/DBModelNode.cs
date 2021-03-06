﻿using ModelTransfer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.model
{
    [Table("DBModelNode")]
    public class DBModelNode
    {
        [Key] public int DBModelNodeID { get; set; }
        public bool isLooped { get; set; }
        public String TypeName { get; set; }
        public String Name { get; set; }
        public String Mods { get; set; }
        public DBModelNode Parent { get; set; }
        public virtual List<DBModelNode> children { get; set; }

        public void create(ModelNodePrototype prot, List<DBModelNode> loaded)
        {
            children = new List<DBModelNode>();
            foreach (ModelNodePrototype p in prot.Nodes)
            {
                bool flaga = false;
                foreach (DBModelNode l in loaded)
                {
                    if (l.Name == p.Name && l.TypeName == p.TypeName)
                    {
                        DBModelNode looped = new DBModelNode();
                        looped.Name = l.Name;
                        looped.TypeName = l.TypeName;
                        looped.Mods = l.Mods;
                        looped.isLooped = true;
                        children.Add(looped);
                        flaga = true;
                        break;
                    }
                }
                if (flaga) continue;
                DBModelNode dbn = new DBModelNode();
                dbn.isLooped = false;
                dbn.Name = p.Name;
                dbn.TypeName = p.TypeName;
                dbn.Mods = p.Mods;
                dbn.Parent = this;
                children.Add(dbn);
                loaded.Add(dbn);
                dbn.create(p, loaded);
            }
        }

        public void retrievePrototype(ModelNodePrototype prot, List<ModelNodePrototype> loaded)
        {
            prot.Nodes = new List<ModelNodePrototype>();
            foreach (DBModelNode p in children)
            {
                if (p.Name == "ServiceA" && p.TypeName == "Class")
                {

                }
                if (p.Name == "ServiceB" && p.TypeName == "Class")
                {

                }
                bool flaga = false;
                foreach (ModelNodePrototype l in loaded)
                {
                    if (l.Name == "ServiceA" && l.TypeName == "Class")
                    {

                    }
                    if (l.Name == "ServiceB" && l.TypeName == "Class")
                    {

                    }
                    if (l.Name == p.Name && l.TypeName == p.TypeName)
                    {
                        prot.Nodes.Add(l);
                        flaga = true;
                        break;
                    }
                }
                if (flaga) continue;
                ModelNodePrototype mp = new ModelNodePrototype();
                mp.Name = p.Name;
                mp.TypeName = p.TypeName;
                mp.Mods = p.Mods;
                mp.Parent = prot;
                prot.Nodes.Add(mp);
                loaded.Add(mp);
                p.retrievePrototype(mp, loaded);
            }
        }
    }
}
