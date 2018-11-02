using Reflection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.ModelTree
{
    class ModelNodeNamespace : ModelNode
    {
        public NamespaceMetaData m_namespace;
        public ModelNodeNamespace(ModelNode parent, NamespaceMetaData m_namespace) : base(parent)
        {
            Name = m_namespace.Name;
            this.m_namespace = m_namespace;
        }

        public override void load()
        {
            nodes.Clear();
            foreach (TypeMetaData t in m_namespace.Types)
            {
                if (t != null) nodes.Add(new ModelNodeType(this, t));
            }
        }
    }
}
