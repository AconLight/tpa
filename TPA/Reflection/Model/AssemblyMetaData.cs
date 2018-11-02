using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Model
{
    public class AssemblyMetadata
    {

        internal AssemblyMetadata(Assembly assembly)
        {
            m_Name = assembly.ManifestModule.Name;
            m_Namespaces = from Type _type in assembly.GetTypes()
                           where _type.GetVisible()
                           group _type by _type.GetNamespace() into _group
                           orderby _group.Key
                           select new NamespaceMetaData(_group.Key, _group);
        }

        private string m_Name;
        private IEnumerable<NamespaceMetaData> m_Namespaces;

    }
}
