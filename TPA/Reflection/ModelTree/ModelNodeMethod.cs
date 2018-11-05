using Reflection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.ModelTree
{
    class ModelNodeMethod : ModelNode
    {
        public MethodMetaData method;
        public ModelNodeMethod(ModelNode parent, MethodMetaData method) : base(parent)
        {
            Name = method.Name;
            TypeName = "Method";
            this.method = method;
        }

        public override void Load()
        {
            isOpen = true;
            nodes.Add(new ModelNodeType(this, method.ReturnType));
            foreach (ParameterMetaData p in method.Parameters)
            {
                nodes.Add(new ModelNodeType(this, p.Type));
            }
        }
    }
}
