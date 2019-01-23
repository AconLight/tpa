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
    public class SerModelNode: ModelNode
    {
        [DataMember]
        public string TypeName;
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Mods { get; set; }
        public ObservableCollection<SerModelNode> MyNodes { get; set; }
        public Boolean IsExpanded { get; set; }

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

        public SerModelNode(ModelNode node) : base(node.Parent, node.Protoype)
        {
            MyNodes = new ObservableCollection<SerModelNode>();
            OnCreate();
        }

        private SerModelNode(ModelNode Parent, ModelNodePrototype Protoype) : base(Parent, Protoype)
        {
            MyNodes = new ObservableCollection<SerModelNode>();
            OnCreate();
        }

        public override void OnCreate()
        {

            Name = Protoype.Name;
            TypeName = Protoype.TypeName;
        }

        public override void OnLoad()
        {
            MyNodes.Clear();
            foreach (ModelNode n in Nodes)
            {
                SerModelNode sn = new SerModelNode(this, n.Protoype);
                MyNodes.Add(sn);
            }
        }

        public void LoadAll()
        {
            List<SerModelNode> loaded = new List<SerModelNode>();
            Load();
            loaded.Add(this);
        }

        public void LoadAll(List<SerModelNode> loaded)
        {
            foreach(SerModelNode s in loaded)
            {
                if(TypeName == s.TypeName && Name == s.Name)
                {
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
    }
}
