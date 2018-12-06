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
    public class ModelNodeNamespace : ModelNode
    {
        public NamespaceMetaData m_namespace;
        [DataMember]
        public ObservableCollection<ModelNodeType> Nodes { get; set; }
        public ModelNodeNamespace(ModelNode parent, NamespaceMetaData m_namespace) : base(parent)
        {
            Nodes = new ObservableCollection<ModelNodeType>();
            Name = m_namespace.Name;
            TypeName = "Namespace";
            this.m_namespace = m_namespace;
        }

        public override void Load(List<ModelNode> loadedNodes)
        {
            foreach (TypeMetaData t in m_namespace.Types)
            {
                if (t != null)
                {
                    Nodes.Add(new ModelNodeType(this, t, "Type"));
                    Nodes.Last().tryLoad(loadedNodes);
                }
            }

        }
        public override void loadAll()
        {
            allNodes = new ObservableCollection<ModelNode>();
            allNodes.Concat(Nodes);
        }
    }
}
