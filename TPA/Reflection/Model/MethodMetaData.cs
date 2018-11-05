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
        
        public static IEnumerable<MethodMetaData> Load(IEnumerable<MethodBase> methods)
        {
            return from method in methods
                   select new MethodMetaData(method.Name, GetReturnType(method),
                        from parameter in method.GetParameters()
                        select new ParameterMetaData(parameter.Name, new TypeMetaData(parameter.GetType()))
                        );
        }

        private string m_Name;
        private TypeMetaData m_ReturnType;
        private IEnumerable<ParameterMetaData> m_Parameters;
        //constructor
        private MethodMetaData(string Name, TypeMetaData ReturnType, IEnumerable<ParameterMetaData> Parameters)
        {
            m_Name = Name;
            m_ReturnType = ReturnType;
            m_Parameters = Parameters;
            
        }
        private static TypeMetaData GetReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            return new TypeMetaData(methodInfo.ReturnType);
        }
    }
}
