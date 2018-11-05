using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Model
{
    public class ParameterMetaData
    {
        public string Name { get => m_Name; set => m_Name = value; }
        public TypeMetaData Type { get => m_TypeMetadata; set => m_TypeMetadata = value; }
        public ParameterMetaData(string name, TypeMetaData typeMetadata)
        {
            this.m_Name = name;
            this.m_TypeMetadata = typeMetadata;
        }

        //private vars
        private string m_Name;
        private TypeMetaData m_TypeMetadata;
    }
}
