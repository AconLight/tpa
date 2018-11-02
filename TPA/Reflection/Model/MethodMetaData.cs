using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Reflection.Model
{
    class MethodMetaData
    {
        internal static IEnumerable<MethodMetaData> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return from MethodBase _currentMethod in methods
                   where _currentMethod.GetVisible()
                   select new MethodMetaData(_currentMethod);
        }

        #region private
        //vars
        private string m_Name;
        //private IEnumerable<TypeMetaData> m_GenericArguments;
        private Tuple<AccessLevelMetaData, AbstractMetaData, StaticlMetaData, VirtuallMetaData> m_Modifiers;
        private TypeMetaData m_ReturnType;
        private bool m_Extension;
        private IEnumerable<ParameterMetaData> m_Parameters;
        //constructor
        private MethodMetaData(MethodBase method)
        {
            m_Name = method.Name;
            //m_GenericArguments = !method.IsGenericMethodDefinition ? null : TypeMetaData.EmitGenericArguments(method.GetGenericArguments());
            m_ReturnType = EmitReturnType(method);
            m_Parameters = EmitParameters(method.GetParameters());
            m_Modifiers = EmitModifiers(method);
            m_Extension = EmitExtension(method);
        }
        //methods
        private static IEnumerable<ParameterMetaData> EmitParameters(IEnumerable<ParameterInfo> parms)
        {
            return from parm in parms
                   select new ParameterMetaData(parm.Name, TypeMetaData.EmitReference(parm.ParameterType));
        }
        private static TypeMetaData EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            return TypeMetaData.EmitReference(methodInfo.ReturnType);
        }
        private static bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }
        private static Tuple<AccessLevelMetaData, AbstractMetaData, StaticlMetaData, VirtuallMetaData> EmitModifiers(MethodBase method)
        {
            AccessLevelMetaData _access = AccessLevelMetaData.IsPrivate;
            if (method.IsPublic)
                _access = AccessLevelMetaData.IsPublic;
            else if (method.IsFamily)
                _access = AccessLevelMetaData.IsProtected;
            else if (method.IsFamilyAndAssembly)
                _access = AccessLevelMetaData.IsProtectedInternal;
            AbstractMetaData _abstract = AbstractMetaData.NotAbstract;
            if (method.IsAbstract)
                _abstract = AbstractMetaData.Abstract;
            StaticlMetaData _static = StaticlMetaData.NotStatic;
            if (method.IsStatic)
                _static = StaticlMetaData.Static;
            VirtuallMetaData _virtual = VirtuallMetaData.NotVirtual;
            if (method.IsVirtual)
                _virtual = VirtuallMetaData.Virtual;
            return new Tuple<AccessLevelMetaData, AbstractMetaData, StaticlMetaData, VirtuallMetaData>(_access, _abstract, _static, _virtual);
        }
        #endregion
    }
}
