using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Model
{
    public class TypeMetaData
    {

        public string Name { get => m_typeName; set => m_typeName = value; }
        public IEnumerable<PropertyMetaData> Properties { get => m_Properties; set => m_Properties = value; }

        private string m_typeName;
        private string m_NamespaceName;
        private TypeMetaData m_BaseType;
        private Tuple<AccessLevelMetaData, SealedMetaData, AbstractMetaData> m_Modifiers;
        private TypeKind m_TypeKind;
        private IEnumerable<Attribute> m_Attributes;
        private IEnumerable<TypeMetaData> m_ImplementedInterfaces;
        private IEnumerable<TypeMetaData> m_NestedTypes;
        private IEnumerable<PropertyMetaData> m_Properties;
        private Type m_Type;
        private TypeMetaData m_DeclaringType;
        private IEnumerable<MethodMetaData> m_Methods;
        private IEnumerable<MethodMetaData> m_Constructors;

        internal TypeMetaData(Type type)
        {
            m_Type = type;
            m_typeName = type.Name;
            m_DeclaringType = EmitDeclaringType(type.DeclaringType);
            //m_Constructors = MethodMetaData.EmitMethods(type.GetConstructors());
            //m_Methods = MethodMetaData.EmitMethods(type.GetMethods());
            m_NestedTypes = EmitNestedTypes(type.GetNestedTypes());
            m_ImplementedInterfaces = EmitImplements(type.GetInterfaces());
            m_Modifiers = EmitModifiers(type);
            m_BaseType = EmitExtends(type.BaseType);
            m_TypeKind = GetTypeKind(type);
            m_Attributes = type.GetCustomAttributes(false).Cast<Attribute>();
        }

        public void LoadProperties()
        {
            m_Properties = PropertyMetaData.Load(m_Type.GetProperties());
        }

        private TypeMetaData(string typeName, string namespaceName)
        {
            m_typeName = typeName;
            m_NamespaceName = namespaceName;
        }

        private IEnumerable<TypeMetaData> EmitNestedTypes(IEnumerable<Type> nestedTypes)
        {
            return from _type in nestedTypes
                   where _type.GetVisible()
                   select new TypeMetaData(_type);
        }

        private IEnumerable<TypeMetaData> EmitImplements(IEnumerable<Type> interfaces)
        {
            return from currentInterface in interfaces
                   select EmitReference(currentInterface);
        }

        internal static TypeMetaData EmitReference(Type type)
        {
            return new TypeMetaData(type.Name, type.GetNamespace());
        }

        private TypeMetaData EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            return EmitReference(declaringType);
        }

        private static TypeMetaData EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            return EmitReference(baseType);
        }

        private static TypeKind GetTypeKind(Type type) //#80 TPA: Reflection - Invalid return value of GetTypeKind() 
        {
            return type.IsEnum ? TypeKind.EnumType :
                   type.IsValueType ? TypeKind.StructType :
                   type.IsInterface ? TypeKind.InterfaceType :
                   TypeKind.ClassType;
        }

        static Tuple<AccessLevelMetaData, SealedMetaData, AbstractMetaData> EmitModifiers(Type type)
        {
            //set defaults 
            AccessLevelMetaData _access = AccessLevelMetaData.IsPrivate;
            AbstractMetaData _abstract = AbstractMetaData.NotAbstract;
            SealedMetaData _sealed = SealedMetaData.NotSealed;
            // check if not default 
            if (type.IsPublic)
                _access = AccessLevelMetaData.IsPublic;
            else if (type.IsNestedPublic)
                _access = AccessLevelMetaData.IsPublic;
            else if (type.IsNestedFamily)
                _access = AccessLevelMetaData.IsProtected;
            else if (type.IsNestedFamANDAssem)
                _access = AccessLevelMetaData.IsProtectedInternal;
            if (type.IsSealed)
                _sealed = SealedMetaData.Sealed;
            if (type.IsAbstract)
                _abstract = AbstractMetaData.Abstract;
            return new Tuple<AccessLevelMetaData, SealedMetaData, AbstractMetaData>(_access, _sealed, _abstract);
        }

        internal enum TypeKind
        {
            EnumType, StructType, InterfaceType, ClassType
        }
    }
}
