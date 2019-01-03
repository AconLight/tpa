using Reflection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ModelTree
{
    class ModelNodeNamespace : ModelNode
    {
        public NamespaceMetaData m_namespace;
        public ModelNodeNamespace(ModelNode parent, NamespaceMetaData m_namespace) : base(parent)
        {
            Name = m_namespace.Name;
            TypeName = "Namespace";
            this.m_namespace = m_namespace;
        }

        public ModelNodeNamespace(ModelNode parent, String Name) : base(parent)
        {
            this.Name = Name;
            TypeName = "Namespace";
        }

        public override void Load()
        {
            IsExpanded = true;
            if(Nodes.Count() == 0 && m_namespace != null)
            foreach (TypeMetaData t in m_namespace.Types)
            {
                if (t != null) Nodes.Add(new ModelNodeType(this, t, "Type"));
            }
        }
    }
}
