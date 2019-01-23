using ModelTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Model
{
    public class PropertyMetaData: ModelNodePrototype
    {
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

        public override void OnCreate()
        {
            Name = m_Name;
            TypeName = "Property";
        }
        public override void OnLoad()
        {
            Nodes = new List<ModelNodePrototype>();
            Nodes.Add(Type);
            Type.Parent = this;
        }
    }
}
