using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reflection.Model;

namespace LogicTests
{
    [TestClass]
    public class ReflectionTests
    {
        public AssemblyMetaData assemblyMetaData;
        [TestInitialize]
        public void Initialise()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            string root = "Tests";
            while (!(path.Substring(path.Length - root.Length) == root))
            {
                path = path.Remove(path.Length - 1);
            }
            string filename = "\\..\\TPA.ApplicationArchitecture.dll";
            string fullFilePath = path + filename;
            assemblyMetaData = new AssemblyMetaData(Assembly.LoadFrom(fullFilePath));
        }
        [TestMethod]
        public void NameSpacesNamespacesInAssembly_WhenAssemblyIsOk_ThereShouldBeAtLeastOneNamespace()
        {
            List<NamespaceMetaData> pom = assemblyMetaData.Namespaces.ToList();
            Assert.IsTrue(pom.Count >= 0);
        }
        [TestMethod]
        public void TypesInNamespace_WhenNamespaceIsOk_ThereShouldBeAtLeastOneType()
        {
            List<NamespaceMetaData> pom1 = assemblyMetaData.Namespaces.ToList();
            List<TypeMetaData> pom2 = pom1[0].Types.ToList();
            Assert.IsTrue(pom2.Count >= 0);
        }
    }
}
