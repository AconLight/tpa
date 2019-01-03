using System;
using System.IO;
using System.Runtime.Serialization;
using Serialization.SerializationModelTree;
using System.Reflection;
using Reflection.Model;
using Reflection;

namespace Serialization
{
    public class Ser: DataBridgeInterface
    {
        String pathToFile = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"ser.xml");
        private Assembly assembly;
        public Ser()
        {
            String path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"TPA.ApplicationArchitecture.dll");
            assembly = Assembly.LoadFrom(path);
        }

        public Ser(String path)
        {
            assembly = Assembly.LoadFrom(path);
        }

        public Ser(Assembly assembly)
        {
            this.assembly = assembly;
        }

        public void serialize(AssemblyMetaData assemblyMetaData)
        {
            ModelTreeHandler tree = new ModelTreeHandler(assemblyMetaData);
            DataContractSerializer s = new DataContractSerializer(typeof(ModelTreeHandler));
            using (FileStream fs = File.Open(pathToFile, FileMode.Create))
            {
                s.WriteObject(fs, tree);
            }
        }

        public AssemblyMetaData deserialize()
        {
            DataContractSerializer s = new DataContractSerializer(typeof(ModelTreeHandler));
            using (FileStream fs = File.Open(pathToFile, FileMode.Open))
            {
                return ((ModelTreeHandler)s.ReadObject(fs)).rootNode.assembly;
            }
        }

        public void write(AssemblyMetaData assemblyMetaData)
        {
            serialize(assemblyMetaData);
        }

        public AssemblyMetaData read()
        {
            return deserialize();
        }
    }

}