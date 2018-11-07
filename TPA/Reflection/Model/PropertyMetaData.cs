using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Model
{
    public class PropertyMetaData
    {
        public string Name { get => m_Name; set => m_Name = value; }
        public TypeMetaData Type { get => m_TypeMetaData; set => m_TypeMetaData = value; }

        internal static IEnumerable<PropertyMetaData> Load(IEnumerable<PropertyInfo> props)
        {
            return from prop in props
                   where prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()
                   select new PropertyMetaData(prop.Name, new TypeMetaData(prop.PropertyType));
        }

        private string m_Name;
        private TypeMetaData m_TypeMetaData;
        private PropertyMetaData(string propertyName, TypeMetaData propertyType)
        {
            m_Name = propertyName;
            m_TypeMetaData = propertyType;
        }
    }
}
