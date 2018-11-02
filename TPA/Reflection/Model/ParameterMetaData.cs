using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Model
{
    class ParameterMetaData
    {
        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            this.m_Name = name;
            this.m_TypeMetadata = typeMetadata;
        }

        //private vars
        private string m_Name;
        private TypeMetadata m_TypeMetadata;
    }
}
