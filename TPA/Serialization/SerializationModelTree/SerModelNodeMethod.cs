using Reflection.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.SerializationModelTree
{
    [DataContract(IsReference = true)]
    public class SerModelNodeMethod : SerModelNode
    {
        public MethodMetaData method;
        [DataMember]
        public ObservableCollection<SerModelNodeType> Nodes { get; set; }
        public SerModelNodeMethod(SerModelNode parent, MethodMetaData method) : base(parent)
        {
            Nodes = new ObservableCollection<SerModelNodeType>();
            this.method = method;
            Name = SubName() + method.Name;
            TypeName = "Method";
        }
        public SerModelNodeMethod(SerModelNode parent, String Name) : base(parent)
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

        public override void Load(List<SerModelNode> loadedNodes)
        {
            if (method.ReturnType != null)
            {
                Nodes.Add(new SerModelNodeType(this, method.ReturnType, "Return Type"));
                Nodes.Last().tryLoad(loadedNodes);
            }
            foreach (ParameterMetaData p in method.Parameters)
            {
                if (p.Type != null)
                {
                    Nodes.Add(new SerModelNodeType(this, p.Type, "Parameter Type"));
                    Nodes.Last().tryLoad(loadedNodes);
                }
            }
           
        }
        public override void loadAll()
        {
            allNodes = new ObservableCollection<SerModelNode>();
            if (Nodes != null)
            foreach (SerModelNode node in Nodes)
            {
                allNodes.Add(node);
            }
        }

        public override void loadNodes()
        {
            Nodes = new ObservableCollection<SerModelNodeType>();
            if (allNodes != null)
                foreach (SerModelNodeType node in allNodes)
                {
                    Nodes.Add(node);
                }
        }
    }
}
