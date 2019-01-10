using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Reflection.Model;

namespace Serialization.SerializationModelTree
{
    [DataContract(IsReference = true)]
    public class SerModelNodeAssembly: SerModelNode
    {
        [DataMember]
        public ObservableCollection<SerModelNodeNamespace> Nodes { get; set; }

        public AssemblyMetaData assembly;
        public SerModelNodeAssembly(SerModelNode parent, AssemblyMetaData assembly): base(parent)
        {
            Nodes = new ObservableCollection<SerModelNodeNamespace>();
            Name = assembly.Name;
            TypeName = "Assembly";
            this.assembly = assembly;
        }
        public SerModelNodeAssembly(SerModelNode parent, String Name) : base(parent)
        {
            Nodes = new ObservableCollection<SerModelNodeNamespace>();
            this.Name = Name;
            TypeName = "Assembly";
        }

        public override void Load(List<SerModelNode> loadedNodes)
        {
            if (assembly != null)
            foreach (NamespaceMetaData n in assembly.Namespaces)
            {
                Nodes.Add(new SerModelNodeNamespace(this, n));
                Nodes.Last().tryLoad(loadedNodes);
            }
            
        }
        public override void loadAll()
        {
            allNodes = new ObservableCollection<SerModelNode>();
            if (Nodes != null)
            foreach(SerModelNode node in Nodes)
            {
                allNodes.Add(node);
            }
        }

        public override void retriveNode()
        {

        }

        public override void loadNodes()
        {
            Nodes = new ObservableCollection<SerModelNodeNamespace>();
            if (allNodes != null)
                foreach (SerModelNodeNamespace node in allNodes)
                {
                    Nodes.Add(node);
                }
        }
    }
}
