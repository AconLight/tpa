using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Model
{
    class ParameterMetaData
    {
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
