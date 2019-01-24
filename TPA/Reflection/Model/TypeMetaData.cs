using ModelTransfer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Model
{
    public class TypeMetaData : ModelNodePrototype
    {

        public IEnumerable<PropertyMetaData> Properties { get => m_Properties; set => m_Properties = value; }
        public IEnumerable<MethodMetaData> Methods { get => m_Methods; set => m_Methods = value; }
        public IEnumerable<MethodMetaData> Constructors { get => m_Constructors; set => m_Constructors = value; }
        public TypeMetaData BaseType { get => m_BaseType; set => m_BaseType = value; }
        public TypeMetaData DeclaringType { get => m_DeclaringType; set => m_DeclaringType = value; }
        public Tuple<AccessLevelMetaData, SealedMetaData, AbstractMetaData> Modifiers { get => m_Modifiers; set => m_Modifiers = value; }
        public IEnumerable<TypeMetaData> NestedTypes { get => m_NestedTypes; set => m_NestedTypes = value; }
        public IEnumerable<TypeMetaData> Interfaces { get => m_Interfaces; set => m_Interfaces = value; }
        public TypeKind TypeKindP { get => m_TypeKind; set => m_TypeKind = value; }
        public IEnumerable<Attribute> Attributes { get => m_Attributes; set => m_Attributes = value; }

        private string m_typeName;
        private Type m_Type;

        private TypeMetaData m_DeclaringType;
        private IEnumerable<TypeMetaData> m_NestedTypes;
        private TypeMetaData m_BaseType;
        private Tuple<AccessLevelMetaData, SealedMetaData, AbstractMetaData> m_Modifiers;
        private TypeKind m_TypeKind;
        private IEnumerable<Attribute> m_Attributes;
        private IEnumerable<TypeMetaData> m_Interfaces;
        private IEnumerable<PropertyMetaData> m_Properties;
        private IEnumerable<MethodMetaData> m_Methods;
        private IEnumerable<MethodMetaData> m_Constructors;

        internal TypeMetaData(Type type)
        {
            m_Type = type;
            m_typeName = type.Name;
            LoadModyfiers();
            LoadTypeKind();
        }

        public void Load()
        {
            if (m_Type.BaseType != null)
                m_BaseType = new TypeMetaData(m_Type.BaseType);
            if (m_Type.DeclaringType != null)
                m_DeclaringType = new TypeMetaData(m_Type.DeclaringType);
            m_Properties = PropertyMetaData.Load(m_Type.GetProperties());
            m_Methods = MethodMetaData.Load(m_Type.GetMethods());
            m_Constructors = MethodMetaData.Load(m_Type.GetConstructors());
            m_Attributes = m_Type.GetCustomAttributesData().Cast<Attribute>();
            LoadNestedTypes();
            LoadInterfaces();
        }

        private void LoadNestedTypes()
        {
            m_NestedTypes = from _type in m_Type.GetNestedTypes()
                   where _type.GetVisible()
                   select new TypeMetaData(_type);
        }

        private void LoadInterfaces()
        {
            m_Interfaces = from currentInterface in m_Type.GetInterfaces()
                   select new TypeMetaData(currentInterface);
        }

        private void LoadTypeKind()
        {
            m_TypeKind = m_Type.IsEnum ? TypeKind.EnumType :
                   m_Type.IsValueType ? TypeKind.StructType :
                   m_Type.IsInterface ? TypeKind.InterfaceType :
                   TypeKind.ClassType;
        }

        private void LoadModyfiers()
        {
            AccessLevelMetaData _access = AccessLevelMetaData.IsPrivate;
            AbstractMetaData _abstract = AbstractMetaData.NotAbstract;
            SealedMetaData _sealed = SealedMetaData.NotSealed;
            if (m_Type.IsPublic)
                _access = AccessLevelMetaData.IsPublic;
            else if (m_Type.IsNestedPublic)
                _access = AccessLevelMetaData.IsPublic;
            else if (m_Type.IsNestedFamily)
                _access = AccessLevelMetaData.IsProtected;
            else if (m_Type.IsNestedFamANDAssem)
                _access = AccessLevelMetaData.IsProtectedInternal;
            if (m_Type.IsSealed)
                _sealed = SealedMetaData.Sealed;
            if (m_Type.IsAbstract)
                _abstract = AbstractMetaData.Abstract;
            m_Modifiers =  new Tuple<AccessLevelMetaData, SealedMetaData, AbstractMetaData>(_access, _sealed, _abstract);
        }

        public enum TypeKind
        {
            EnumType, StructType, InterfaceType, ClassType
        }

        public override void OnCreate()
        {
            Name = m_typeName;
            TypeName = "Class";
            Mods = Modifiers.Item1.ToString();
            Mods += " ";
            Mods += Modifiers.Item2.ToString();
            Mods += " ";
            Mods += Modifiers.Item3.ToString();
        }
        public override void OnLoad()
        {
            Load();
            Nodes = new List<ModelNodePrototype>();
            foreach (ModelNodePrototype prot in Properties)
            {
                Nodes.Add(prot);
                prot.Parent = this;
            }
            foreach (ModelNodePrototype prot in Methods)
            {
                Nodes.Add(prot);
                prot.Parent = this;
            }
            foreach (ModelNodePrototype prot in Constructors)
            {
                Nodes.Add(prot);
                prot.Parent = this;
            }
            foreach (ModelNodePrototype prot in Interfaces)
            {
                Nodes.Add(prot);
                prot.Parent = this;
            }
            foreach (ModelNodePrototype prot in NestedTypes)
            {
                Nodes.Add(prot);
                prot.Parent = this;
            }
        }
    }
}
