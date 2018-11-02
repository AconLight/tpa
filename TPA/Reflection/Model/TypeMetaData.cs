using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Model
{
    internal class TypeMetaData
    {
        private string m_typeName;
        private string m_NamespaceName;
        private TypeMetaData m_BaseType;
        private Tuple<AccessLevelMetaData, SealedMetaData, AbstractENum> m_Modifiers;
        private TypeKind m_TypeKind;
        private IEnumerable<Attribute> m_Attributes;
        private IEnumerable<TypeMetaData> m_ImplementedInterfaces;
        private IEnumerable<TypeMetaData> m_NestedTypes;
        private IEnumerable<PropertyMetaData> m_Properties;
        private TypeMetaData m_DeclaringType;
        private IEnumerable<MethodMetaData> m_Methods;
        private IEnumerable<MethodMetaData> m_Constructors;

        internal TypeMetaData(Type type)
        {
            m_typeName = type.Name;
           /* m_DeclaringType = EmitDeclaringType(type.DeclaringType);
            m_Constructors = MethodMetadata.EmitMethods(type.GetConstructors());
            m_Methods = MethodMetadata.EmitMethods(type.GetMethods());
            m_NestedTypes = EmitNestedTypes(type.GetNestedTypes());
            m_ImplementedInterfaces = EmitImplements(type.GetInterfaces());
            m_GenericArguments = !type.IsGenericTypeDefinition ? null : TypeMetadata.EmitGenericArguments(type.GetGenericArguments());
            m_Modifiers = EmitModifiers(type);
            m_BaseType = EmitExtends(type.BaseType);
            m_Properties = PropertyMetadata.EmitProperties(type.GetProperties());
            m_TypeKind = GetTypeKind(type);
            m_Attributes = type.GetCustomAttributes(false).Cast<Attribute>();*/
        }

        internal enum TypeKind
        {
            EnumType, StructType, InterfaceType, ClassType
        }
    }
}
