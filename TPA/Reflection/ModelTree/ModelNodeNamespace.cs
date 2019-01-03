using Reflection.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.ModelTree
{

    public class ModelNodeNamespace : ModelNode
    {
        public NamespaceMetaData m_namespace;

        public ObservableCollection<ModelNodeType> Nodes { get; set; }
        public ModelNodeNamespace(ModelNode parent, NamespaceMetaData m_namespace) : base(parent)
        {
            Nodes = new ObservableCollection<ModelNodeType>();
            Name = m_namespace.Name;
            TypeName = "Namespace";
            this.m_namespace = m_namespace;
        }

        public ModelNodeNamespace(ModelNode parent, String Name) : base(parent)
        {
            this.Name = Name;
            TypeName = "Namespace";
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
            foreach (ModelNode node in Nodes)
            {
                allNodes.Add(node);
            }
        }
    }
}
