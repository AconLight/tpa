using Reflection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.ModelTree
{
    class ModelNodeType : ModelNode
    {
        public TypeMetaData type;
        public ModelNodeType(ModelNode parent, TypeMetaData type) : base(parent)
        {
            Name = type.Name;
            TypeName = "Class";
            this.type = type;
            type.Load();
        }

        public override void Load()
        {
            isOpen = true;
            foreach (PropertyMetaData p in type.Properties)
            {
                nodes.Add(new ModelNodeType(this, p.Type));
            }
            foreach (MethodMetaData m in type.Methods)
            {
                nodes.Add(new ModelNodeMethod(this, m));
            }
        }
    }
}
