using Reflection.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelTree
{

    public class LogicModelNodeType : LogicModelNode
    {
        public TypeMetaData type;

        public ObservableCollection<LogicModelNodeType> TypeNodes { get; set; }

        public ObservableCollection<LogicModelNodeMethod> MethodNodes { get; set; }

        public LogicModelNodeType(LogicModelNode parent, TypeMetaData type, string TypeName) : base(parent)
        {
            MethodNodes = new ObservableCollection<LogicModelNodeMethod>();
            TypeNodes = new ObservableCollection<LogicModelNodeType>();
            this.TypeName = TypeName;
            this.type = type;
            Name = SubName() + type.Name;
            type.Load();
        }

        public LogicModelNodeType(LogicModelNode parent, string Name, string TypeName) : base(parent)
        {
            MethodNodes = new ObservableCollection<LogicModelNodeMethod>();
            TypeNodes = new ObservableCollection<LogicModelNodeType>();
            this.TypeName = TypeName;
            this.Name = Name;
        }

        private string SubName()
        {
            string access = "";
            if (type.Modifiers.Item1 == AccessLevelMetaData.IsPrivate) access = "private ";
            else if (type.Modifiers.Item1 == AccessLevelMetaData.IsProtected) access = "protected ";
            else if (type.Modifiers.Item1 == AccessLevelMetaData.IsProtectedInternal) access = "protected internal ";
            else if (type.Modifiers.Item1 == AccessLevelMetaData.IsPublic) access = "public ";

            string mySealed = "";
            if (type.Modifiers.Item2 == SealedMetaData.Sealed) mySealed = "sealed ";

            string myAbstract = "";
            if (type.Modifiers.Item3 == AbstractMetaData.Abstract) myAbstract = "abstract ";

            string typeKind = "";
            if (type.TypeKindP == TypeMetaData.TypeKind.ClassType) typeKind = "class ";
            else if (type.TypeKindP == TypeMetaData.TypeKind.EnumType) typeKind = "enum ";
            else if (type.TypeKindP == TypeMetaData.TypeKind.InterfaceType) typeKind = "interface ";
            else if (type.TypeKindP == TypeMetaData.TypeKind.StructType) typeKind = "struct ";

            return access + mySealed + myAbstract + typeKind;
        }

        public override void Load(List<LogicModelNode> loadedNodes)
        {
            foreach (PropertyMetaData p in type.Properties)
            {
                if (p != null && p.Type != null)
                {
                    TypeNodes.Add(new LogicModelNodeType(this, p.Type, "Property"));
                    TypeNodes.Last().tryLoad(loadedNodes);
                }
            }
            foreach (MethodMetaData m in type.Methods)
            {
                if (m != null)
                {
                    MethodNodes.Add(new LogicModelNodeMethod(this, m));
                    MethodNodes.Last().tryLoad(loadedNodes);
                }
            }
            foreach (MethodMetaData m in type.Constructors)
            {
                if (m != null)
                {
                    MethodNodes.Add(new LogicModelNodeMethod(this, m));
                    MethodNodes.Last().tryLoad(loadedNodes);
                }
            }
            foreach (TypeMetaData m in type.Interfaces)
            {
                if (m != null)
                {
                    TypeNodes.Add(new LogicModelNodeType(this, m, "Interface"));
                    TypeNodes.Last().tryLoad(loadedNodes);
                }
            }
            foreach (TypeMetaData m in type.NestedTypes)
            {
                if (m != null)
                {
                    TypeNodes.Add(new LogicModelNodeType(this, m, "Nested Type"));
                    TypeNodes.Last().tryLoad(loadedNodes);
                }
            }
            if (type.BaseType != null)
            {
                TypeNodes.Add(new LogicModelNodeType(this, type.BaseType, "Base Type"));
                TypeNodes.Last().tryLoad(loadedNodes);
            }
            if (type.DeclaringType != null)
            {
                TypeNodes.Add(new LogicModelNodeType(this, type.DeclaringType, "Declaring Type"));
                TypeNodes.Last().tryLoad(loadedNodes);
            }
            loadAll();
        }
        public override void loadAll()
        {
            allNodes = new ObservableCollection<LogicModelNode>();
            foreach (LogicModelNode node in TypeNodes)
            {
                allNodes.Add(node);
            }
            foreach (LogicModelNode node in MethodNodes)
            {
                allNodes.Add(node);
            }
        }

        public override void loadNodes()
        {
            MethodNodes = new ObservableCollection<LogicModelNodeMethod>();
            TypeNodes = new ObservableCollection<LogicModelNodeType>();
            foreach (LogicModelNode node in allNodes)
            {
                if (node.TypeName == "Method")
                {
                    MethodNodes.Add((LogicModelNodeMethod)node);
                }
                else
                {
                    TypeNodes.Add((LogicModelNodeType)node);
                }
                
            }
        }
    }
}
