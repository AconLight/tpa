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
            this.type = type;
            type.LoadProperties();
        }

        public override void Load()
        {
            isOpen = true;
            if (type != null && type.Properties != null)
            foreach (PropertyMetaData p in type.Properties)
            {
                if (p.Type != null) nodes.Add(new ModelNodeType(this, p.Type));
            }
        }
    }
}
