using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Reflection.Model
{
    public class MethodMetaData
    {

        public string Name { get => m_Name; set => m_Name = value; }
        public IEnumerable<ParameterMetaData> Parameters { get => m_Parameters; set => m_Parameters = value; }
        public TypeMetaData ReturnType { get => m_ReturnType; set => m_ReturnType = value; }
        public Tuple<AccessLevelMetaData, SealedMetaData, VirtuallMetaData> Modifiers { get => m_Modifiers; set => m_Modifiers = value; }

        public static IEnumerable<MethodMetaData> Load(IEnumerable<MethodBase> methods)
        {
            return from method in methods
                   select new MethodMetaData(method);
        }

        private string m_Name;
        private TypeMetaData m_ReturnType;
        private IEnumerable<ParameterMetaData> m_Parameters;
        private MethodBase m_method;
        private Tuple<AccessLevelMetaData, SealedMetaData, VirtuallMetaData> m_Modifiers;
        private MethodMetaData(MethodBase method)
        {
            m_method = method;
            m_Name = method.Name;
            m_ReturnType = GetReturnType(method);
            m_Parameters = from parameter in method.GetParameters()
                           select new ParameterMetaData(parameter.Name, new TypeMetaData(parameter.GetType()));
            LoadModyfiers();
        }
        private static TypeMetaData GetReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            return new TypeMetaData(methodInfo.ReturnType);
        }

        private void LoadModyfiers()
        {
            AccessLevelMetaData _access = AccessLevelMetaData.IsPrivate;
            SealedMetaData _sealed = SealedMetaData.NotSealed;
            VirtuallMetaData _virtual = VirtuallMetaData.NotVirtual;
            if (m_method.IsPublic)
                _access = AccessLevelMetaData.IsPublic;
            else if (m_method.IsPrivate)
                _access = AccessLevelMetaData.IsPublic;
            else if (m_method.IsFamily)
                _access = AccessLevelMetaData.IsProtected;
            else if (m_method.IsFamilyAndAssembly)
                _access = AccessLevelMetaData.IsProtectedInternal;
            if (m_method.IsVirtual && m_method.IsFinal)
                _sealed = SealedMetaData.Sealed;
            if (m_method.IsAbstract)
                _virtual = VirtuallMetaData.Virtual;
            m_Modifiers = new Tuple<AccessLevelMetaData, SealedMetaData, VirtuallMetaData>(_access, _sealed, _virtual);
        }
    }
}
