using Reflection.Model;
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
    public class SerModelNodeNamespace : SerModelNode
    {
        public NamespaceMetaData m_namespace;
        [DataMember]
        public ObservableCollection<SerModelNodeType> Nodes { get; set; }
        public SerModelNodeNamespace(SerModelNode parent, NamespaceMetaData m_namespace) : base(parent)
        {
            Nodes = new ObservableCollection<SerModelNodeType>();
            Name = m_namespace.Name;
            TypeName = "Namespace";
            this.m_namespace = m_namespace;
        }

        public SerModelNodeNamespace(SerModelNode parent, String Name) : base(parent)
        {
            this.Name = Name;
            TypeName = "Namespace";
        }

        public override void Load(List<SerModelNode> loadedNodes)
        {
            foreach (TypeMetaData t in m_namespace.Types)
            {
                if (t != null)
                {
                    Nodes.Add(new SerModelNodeType(this, t, "Type"));
                    Nodes.Last().tryLoad(loadedNodes);
                }
            }

        }
        public override void loadAll()
        {
            allNodes = new ObservableCollection<SerModelNode>();
            foreach (SerModelNode node in Nodes)
            {
                allNodes.Add(node);
            }
        }

        public override void loadNodes()
        {
            Nodes = new ObservableCollection<SerModelNodeType>();
            if (allNodes != null)
                foreach (SerModelNodeType node in allNodes)
                {
                    Nodes.Add(node);
                }
        }
    }
}
