using ModelTransfer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.SerializationModelTree
{
    [DataContract(IsReference = true)]
    public class SerContainer
    {

        [DataMember]
        public SerModelNode root { get; set; }

        public SerContainer(SerModelNode root)
        {
            this.root = root;
        }

        public void restorePrototypes()
        {
            List<ModelNodePrototype> restored = new List<ModelNodePrototype>();
            root.Protoype = new ModelNodePrototype();
            root.Protoype.Name = root.Name;
            root.Protoype.TypeName = root.TypeName;
            root.Protoype.Mods = root.Mods;
            root.Protoype.Nodes = new List<ModelNodePrototype>();
            foreach(SerModelNode s in root.MyNodes)
            {
                root.Protoype.Nodes.Add(s.Protoype = new ModelNodePrototype());
            }
            root.restorePrototype(restored);
            Debug.WriteLine("dddd");
            Debug.WriteLine(root.Protoype.Nodes[0].Name);
            Debug.WriteLine("cccc");
            Debug.WriteLine(root.MyNodes[0].Name);
        }
    }
}
