using System;
using System.IO;
using System.Runtime.Serialization;
using System.ComponentModel.Composition;
using System.Reflection;
using Reflection;
using ModelTransfer;
using Reflection.Model;

namespace Serialization
{
    [Export("XMLser",typeof(DataBridgeInterface))]
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

        public void serialize(AssemblyMetaData root)
        {
            /*LogicModelTreeHandler rtree = logicTree;
            SerializationModelTree.SerModelTreeHandler tree = SerializationModelTree.SerModelTreeHandler.createSerModelTreeToSer(rtree);
            DataContractSerializer s = new DataContractSerializer(typeof(SerializationModelTree.SerModelTreeHandler));
            using (FileStream fs = File.Open(pathToFile, FileMode.Create))
            {
                s.WriteObject(fs, tree);
            }*/
        }

        public AssemblyMetaData deserialize()
        {
            DataContractSerializer s = new DataContractSerializer(typeof(SerializationModelTree.SerModelTreeHandler));
            using (FileStream fs = File.Open(pathToFile, FileMode.Open))
            {
                SerializationModelTree.SerModelTreeHandler tree = ((SerializationModelTree.SerModelTreeHandler)s.ReadObject(fs));
                return SerializationModelTree.SerModelTreeHandler.createLogicModelTree(tree);
            }
        }

        public void save(ModelNodePrototype assembly)
        {
            throw new NotImplementedException();
        }

        public ModelNodePrototype load()
        {
            throw new NotImplementedException();
        }
    }

}