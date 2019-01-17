using System;
using System.IO;
using System.Runtime.Serialization;
using System.ComponentModel.Composition;
using Data;
using Data.ModelTree;
using System.Reflection;

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

        public void serialize(LogicModelTreeHandler logicTree)
        {
            LogicModelTreeHandler rtree = logicTree;
            SerializationModelTree.SerModelTreeHandler tree = SerializationModelTree.SerModelTreeHandler.createSerModelTreeToSer(rtree);
            DataContractSerializer s = new DataContractSerializer(typeof(SerializationModelTree.SerModelTreeHandler));
            using (FileStream fs = File.Open(pathToFile, FileMode.Create))
            {
                s.WriteObject(fs, tree);
            }
        }

        public LogicModelTreeHandler deserialize()
        {
            DataContractSerializer s = new DataContractSerializer(typeof(SerializationModelTree.SerModelTreeHandler));
            using (FileStream fs = File.Open(pathToFile, FileMode.Open))
            {
                SerializationModelTree.SerModelTreeHandler tree = ((SerializationModelTree.SerModelTreeHandler)s.ReadObject(fs));
                return SerializationModelTree.SerModelTreeHandler.createLogicModelTree(tree);
            }
        }

        public void write(LogicModelTreeHandler tree)
        {
            serialize(tree);
        }

        public LogicModelTreeHandler read()
        {
            return deserialize();
        }
    }

}