using ModelTransfer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.SerializationModelTree
{
    [DataContract(IsReference = true)]
    public class SerModelNode
    {
        [DataMember]
        public string TypeName;
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Mods { get; set; }
        [DataMember]
        public ObservableCollection<SerModelNode> MyNodes { get; set; }
        public Boolean IsExpanded { get; set; }
        [DataMember]
        SerModelNode Parent;
        public ModelNodePrototype Protoype;
        public List<SerModelNode> Nodes;
        public bool OpenClose
        {
            get { return IsExpanded; }
            set
            {
                if (value)
                {
                    IsExpanded = true;
                    Load();
                }
                else { IsExpanded = false; }
            }
        }
        //root
        public SerModelNode(ModelNode node)
        {
            this.Parent = null;
            this.Protoype = node.Protoype;
            Nodes = new List<SerModelNode>();
            MyNodes = new ObservableCollection<SerModelNode>();
            OnCreate();
        }
        public SerModelNode(SerModelNode node)
        {
            this.Parent = node.Parent;
            this.Protoype = node.Protoype;
            Nodes = new List<SerModelNode>();
            MyNodes = new ObservableCollection<SerModelNode>();
            OnCreate();
        }

        private SerModelNode(SerModelNode Parent, ModelNodePrototype Protoype)
        {
            this.Parent = Parent;
            this.Protoype = Protoype;
            Nodes = new List<SerModelNode>();
            MyNodes = new ObservableCollection<SerModelNode>();
            OnCreate();
        }

        public void OnCreate()
        {

            Name = Protoype.Name;
            TypeName = Protoype.TypeName;
            Mods = Protoype.Mods;
        }

        public void OnLoad()
        {
            MyNodes.Clear();
            foreach (SerModelNode n in Nodes)
            {
                SerModelNode sn = new SerModelNode(this, n.Protoype);
                MyNodes.Add(sn);
            }
        }

        public SerModelNode LoadAll()
        {
            List<SerModelNode> loaded = new List<SerModelNode>();
            Load();
            loaded.Add(this);
            foreach (SerModelNode s in MyNodes)
            {
                s.LoadAll(loaded);
            }
            return this;
        }

        public void LoadAll(List<SerModelNode> loaded)
        {
            foreach(SerModelNode s in loaded)
            {
                if(TypeName == s.TypeName && Name == s.Name)
                {
                    foreach(SerModelNode n in s.MyNodes)
                    {
                        MyNodes.Add(n);
                    }
                    return;
                }
            }
            Load();
            loaded.Add(this);
            foreach (SerModelNode s in MyNodes)
            {
                s.LoadAll(loaded);
            }
        }

        public void Load()
        {
            LoadNodesFromProt();
            OnLoad();
        }

        public void LoadNodesFromProt()
        {
            Nodes.Clear();
            foreach (ModelNodePrototype p in Protoype.Nodes)
            {
                Nodes.Add(new SerModelNode(this, p));
            }
        }

        public void restorePrototype(List<ModelNodePrototype> restored)
        {

            foreach (SerModelNode s in MyNodes)
            {
                s.Protoype.Parent = Protoype;
                s.Protoype.Name = s.Name;
                s.Protoype.TypeName = s.TypeName;
                s.Protoype.Mods = s.Mods;
                s.Protoype.Nodes = new List<ModelNodePrototype>();
                bool flaga = false;

                foreach (ModelNodePrototype r in restored)
                {
                    if (r.Name == s.Name && r.TypeName == s.TypeName)
                    {
                        s.Protoype.Nodes = new List<ModelNodePrototype>();
                        foreach (ModelNodePrototype r2 in r.Nodes)
                        {
                            s.Protoype.Nodes.Add(r2);
                        }
                        flaga = true;
                        break;
                    }
                }
                if (flaga)
                {
                    continue;
                }
                s.Protoype.Nodes = new List<ModelNodePrototype>();
                foreach (SerModelNode s2 in s.MyNodes)
                {
                    s.Protoype.Nodes.Add(s2.Protoype = new ModelNodePrototype());
                }
                restored.Add(s.Protoype);
                s.restorePrototype(restored);
            }
        }
    }
}
