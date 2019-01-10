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
    public class LogicModelNodeMethod : LogicModelNode
    {
        public MethodMetaData method;
        public ObservableCollection<LogicModelNodeType> Nodes { get; set; }
        public LogicModelNodeMethod(LogicModelNode parent, MethodMetaData method) : base(parent)
        {
            Nodes = new ObservableCollection<LogicModelNodeType>();
            this.method = method;
            Name = SubName() + method.Name;
            TypeName = "Method";
        }
        public LogicModelNodeMethod(LogicModelNode parent, String Name) : base(parent)
        {
            Nodes = new ObservableCollection<LogicModelNodeType>();
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

        public override void Load(List<LogicModelNode> loadedNodes)
        {
            if (method.ReturnType != null)
            {
                Nodes.Add(new LogicModelNodeType(this, method.ReturnType, "Return Type"));
                Nodes.Last().tryLoad(loadedNodes);
            }
            foreach (ParameterMetaData p in method.Parameters)
            {
                if (p.Type != null)
                {
                    Nodes.Add(new LogicModelNodeType(this, p.Type, "Parameter Type"));
                    Nodes.Last().tryLoad(loadedNodes);
                }
            }
            loadAll();

        }
        public override void loadAll()
        {
            allNodes = new ObservableCollection<LogicModelNode>();
            foreach (LogicModelNode node in Nodes)
            {
                allNodes.Add(node);
            }
        }

        public override void loadNodes()
        {
            Nodes = new ObservableCollection<LogicModelNodeType>();
            foreach (LogicModelNode node in allNodes)
            {
                Nodes.Add((LogicModelNodeType)node);
            }
        }
    }
}
