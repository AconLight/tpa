using Reflection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ModelTree
{
    class ModelNodeMethod : ModelNode
    {
        public MethodMetaData method;
        public ModelNodeMethod(ModelNode parent, MethodMetaData method) : base(parent)
        {
            this.method = method;
            Name = SubName() + method.Name;
            TypeName = "Method";
        }

        public ModelNodeMethod(ModelNode parent, String Name) : base(parent)
        {
            this.Name = Name;
            TypeName = "Method";
        }

        private string SubName()
        {
            string access = "";
            if (method.Modifiers.Item1 == AccessLevelMetaData.IsPrivate) access = "private ";
            else if (method.Modifiers.Item1 == AccessLevelMetaData.IsProtected) access = "protected ";
            else if (method.Modifiers.Item1 == AccessLevelMetaData.IsProtectedInternal) access = "protected internal ";
            else if (method.Modifiers.Item1 == AccessLevelMetaData.IsPublic) access = "public ";

            string mySealed = "";
            if (method.Modifiers.Item2 == SealedMetaData.Sealed) mySealed = "sealed ";

            string myVirtual = "";
            if (method.Modifiers.Item3 == VirtuallMetaData.Virtual) myVirtual = "virtual ";

            return access + mySealed + myVirtual;
        }

        public override void Load()
        {
            IsExpanded = true;
            if (Nodes.Count() == 0)
            {
                if (method.ReturnType != null)
                    Nodes.Add(new ModelNodeType(this, method.ReturnType, "Return Type"));
                foreach (ParameterMetaData p in method.Parameters)
                {
                    if (p.Type != null)
                        Nodes.Add(new ModelNodeType(this, p.Type, "Parameter Type"));
                }
            }
        }
    }
}
