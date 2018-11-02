using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Model
{
    internal class PropertyMetaData
    {

        internal static IEnumerable<PropertyMetaData> EmitProperties(IEnumerable<PropertyInfo> props)
        {
            return from prop in props
                   where prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()
                   select new PropertyMetaData(prop.Name, TypeMetaData.EmitReference(prop.PropertyType));
        }

        #region private
        private string m_Name;
        private TypeMetaData m_TypeMetaData;
        private PropertyMetaData(string propertyName, TypeMetaData propertyType)
        {
            m_Name = propertyName;
            m_TypeMetaData = propertyType;
        }
        #endregion

    }
}
