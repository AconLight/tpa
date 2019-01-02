using Reflection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ModelTree
{
    class ModelNodeType : ModelNode
    {
        public TypeMetaData type;
        public ModelNodeType(ModelNode parent, TypeMetaData type, string TypeName) : base(parent)
        {

            this.TypeName = TypeName;
            this.type = type;
            Name = SubName() + type.Name;
            type.Load();
        }

        public ModelNodeType(ModelNode parent, string Name, string TypeName) : base(parent)
        {

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

        public override void Load()
        {
            IsExpanded = true;
            if (Nodes.Count() == 0)
            {
                foreach (PropertyMetaData p in type.Properties)
                {
                    if (p != null && p.Type != null)
                        Nodes.Add(new ModelNodeType(this, p.Type, "Property"));
                }
                foreach (MethodMetaData m in type.Methods)
                {
                    if (m != null)
                        Nodes.Add(new ModelNodeMethod(this, m));
                }
                foreach (MethodMetaData m in type.Constructors)
                {
                    if (m != null)
                        Nodes.Add(new ModelNodeMethod(this, m));
                }
                foreach (TypeMetaData m in type.Interfaces)
                {
                    if (m != null)
                        Nodes.Add(new ModelNodeType(this, m, "Interface"));
                }
                foreach (TypeMetaData m in type.NestedTypes)
                {
                    if (m != null)
                        Nodes.Add(new ModelNodeType(this, m, "Nested Type"));
                }
                if (type.BaseType != null)
                    Nodes.Add(new ModelNodeType(this, type.BaseType, "Base Type"));
                if (type.DeclaringType != null)
                    Nodes.Add(new ModelNodeType(this, type.DeclaringType, "Declaring Type"));
            }
        }
    }
}
