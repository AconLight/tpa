using ModelTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Model
{
    public class NamespaceMetaData : ModelNodePrototype
    {
        private string m_NamespaceName;
        private IEnumerable<TypeMetaData> m_Types;

        internal NamespaceMetaData(string name, IEnumerable<Type> types)
        {
            m_NamespaceName = name;
            m_Types = from type in types orderby type.Name select new TypeMetaData(type);
        }
        public IEnumerable<TypeMetaData> Types { get => m_Types; set => m_Types = value; }

        public override void OnCreate()
        {
            Name = m_NamespaceName;
            TypeName = "Namespace";
        }
        public override void OnLoad()
        {
            Nodes = new List<ModelNodePrototype>();
            foreach (ModelNodePrototype prot in Types)
            {
                Nodes.Add(prot);
                prot.Parent = this;
            }
        }
    }
}
