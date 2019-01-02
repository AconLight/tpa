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
    public class ModelNodeType : ModelNode
    {
        public TypeMetaData type;
        [DataMember]
        public ObservableCollection<ModelNodeType> TypeNodes { get; set; }
        [DataMember]
        public ObservableCollection<ModelNodeMethod> MethodNodes { get; set; }

        public ModelNodeType(ModelNode parent, TypeMetaData type, string TypeName) : base(parent)
        {
            MethodNodes = new ObservableCollection<ModelNodeMethod>();
            TypeNodes = new ObservableCollection<ModelNodeType>();
            this.TypeName = TypeName;
            this.type = type;
            Name = SubName() + type.Name;
            type.Load();
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

        public override void Load(List<ModelNode> loadedNodes)
        {
            foreach (PropertyMetaData p in type.Properties)
            {
                if (p != null && p.Type != null)
                {
                    TypeNodes.Add(new ModelNodeType(this, p.Type, "Property"));
                    TypeNodes.Last().tryLoad(loadedNodes);
                }
            }
            foreach (MethodMetaData m in type.Methods)
            {
                if (m != null)
                {
                    MethodNodes.Add(new ModelNodeMethod(this, m));
                    MethodNodes.Last().tryLoad(loadedNodes);
                }
            }
            foreach (MethodMetaData m in type.Constructors)
            {
                if (m != null)
                {
                    MethodNodes.Add(new ModelNodeMethod(this, m));
                    MethodNodes.Last().tryLoad(loadedNodes);
                }
            }
            foreach (TypeMetaData m in type.Interfaces)
            {
                if (m != null)
                {
                    TypeNodes.Add(new ModelNodeType(this, m, "Interface"));
                    TypeNodes.Last().tryLoad(loadedNodes);
                }
            }
            foreach (TypeMetaData m in type.NestedTypes)
            {
                if (m != null)
                {
                    TypeNodes.Add(new ModelNodeType(this, m, "Nested Type"));
                    TypeNodes.Last().tryLoad(loadedNodes);
                }
            }
            if (type.BaseType != null)
            {
                TypeNodes.Add(new ModelNodeType(this, type.BaseType, "Base Type"));
                TypeNodes.Last().tryLoad(loadedNodes);
            }
            if (type.DeclaringType != null)
            {
                TypeNodes.Add(new ModelNodeType(this, type.DeclaringType, "Declaring Type"));
                TypeNodes.Last().tryLoad(loadedNodes);
            }

        }
        public override void loadAll()
        {
            allNodes = new ObservableCollection<ModelNode>();
            foreach (ModelNode node in TypeNodes)
            {
                allNodes.Add(node);
            }
            foreach (ModelNode node in MethodNodes)
            {
                allNodes.Add(node);
            }
        }
    }
}
