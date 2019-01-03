using System;
using System.IO;
using System.Runtime.Serialization;
using Serialization.SerializationModelTree;
using System.Reflection;
using Reflection.Model;
using Reflection;
using Reflection.ModelTree;
using Serialization.SerializationModelTree;

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

        public void serialize(Reflection.ModelTree.ModelNodeAssembly modelNodeAssembly)
        {
            Reflection.ModelTree.ModelTreeHandler rtree = new Reflection.ModelTree.ModelTreeHandler(modelNodeAssembly);
            SerializationModelTree.ModelTreeHandler tree = SerializationModelTree.ModelTreeHandler.createModelTreeToSer(rtree);
            DataContractSerializer s = new DataContractSerializer(typeof(SerializationModelTree.ModelTreeHandler));
            using (FileStream fs = File.Open(pathToFile, FileMode.Create))
            {
                s.WriteObject(fs, tree);
            }
        }

        public Reflection.ModelTree.ModelNodeAssembly deserialize()
        {
            DataContractSerializer s = new DataContractSerializer(typeof(SerializationModelTree.ModelTreeHandler));
            using (FileStream fs = File.Open(pathToFile, FileMode.Open))
            {
                SerializationModelTree.ModelTreeHandler tree = ((SerializationModelTree.ModelTreeHandler)s.ReadObject(fs));
                return SerializationModelTree.ModelTreeHandler.createModelTree(tree).rootNode;
            }
        }

        public void write(Reflection.ModelTree.ModelNodeAssembly assemblyMetaData)
        {
            serialize(assemblyMetaData);
        }

        Reflection.ModelTree.ModelNodeAssembly DataBridgeInterface.read()
        {
            return deserialize();
        }
    }

}