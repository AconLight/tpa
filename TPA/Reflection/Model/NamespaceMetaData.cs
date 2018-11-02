using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Model
{
    public class NamespaceMetaData
    {
        private string m_NamespaceName;
        private IEnumerable<TypeMetaData> m_Types;

        internal NamespaceMetaData(string name, IEnumerable<Type> types)
        {
            m_NamespaceName = name;
            m_Types = from type in types orderby type.Name select new TypeMetaData(type);
        }
        public string Name { get => m_NamespaceName; set => m_NamespaceName = value; }
        public IEnumerable<TypeMetaData> Types { get => m_Types; set => m_Types = value; }
    }
}
