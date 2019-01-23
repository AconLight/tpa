using ModelTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Model
{
    public class ParameterMetaData: ModelNodePrototype
    {
        public TypeMetaData Type { get => m_TypeMetadata; set => m_TypeMetadata = value; }
        public ParameterMetaData(string name, TypeMetaData typeMetadata)
        {
            this.m_Name = name;
            this.m_TypeMetadata = typeMetadata;
        }

        //private vars
        private string m_Name;
        private TypeMetaData m_TypeMetadata;

        public override void OnCreate()
        {
            Name = m_Name;
            TypeName = "Parameter";
            Mods = "";
        }
        public override void OnLoad()
        {
            Nodes = new List<ModelNodePrototype>();
            Nodes.Add(Type);
            Type.Parent = this;
        }
    }
}
