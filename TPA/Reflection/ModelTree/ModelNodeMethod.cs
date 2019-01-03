using Reflection.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.ModelTree
{
    public class ModelNodeMethod : ModelNode
    {
        public MethodMetaData method;
        public ObservableCollection<ModelNodeType> Nodes { get; set; }
        public ModelNodeMethod(ModelNode parent, MethodMetaData method) : base(parent)
        {
            Nodes = new ObservableCollection<ModelNodeType>();
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

        public override void Load(List<ModelNode> loadedNodes)
        {
            if (method.ReturnType != null)
            {
                Nodes.Add(new ModelNodeType(this, method.ReturnType, "Return Type"));
                Nodes.Last().tryLoad(loadedNodes);
            }
            foreach (ParameterMetaData p in method.Parameters)
            {
                if (p.Type != null)
                {
                    Nodes.Add(new ModelNodeType(this, p.Type, "Parameter Type"));
                    Nodes.Last().tryLoad(loadedNodes);
                }
            }
           
        }
        public override void loadAll()
        {
            allNodes = new ObservableCollection<ModelNode>();
            foreach (ModelNode node in Nodes)
            {
                allNodes.Add(node);
            }
        }
    }
}
