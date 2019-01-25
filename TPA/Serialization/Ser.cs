using System;
using System.IO;
using System.Runtime.Serialization;
using System.ComponentModel.Composition;
using ModelTransfer;
using Serialization.SerializationModelTree;
using System.Diagnostics;

namespace Serialization
{
    [Export("XMLSer",typeof(DataBridgeInterface))]
    public class Ser : DataBridgeInterface
    {
        String pathToFile = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"ser.xml");

        public void serialize(ModelNodePrototype root)
        {

            SerModelNode serRoot = new SerModelNode(new ModelNode(null, root));
            SerContainer sc = new SerContainer(serRoot.LoadAll());
            DataContractSerializer s = new DataContractSerializer(typeof(SerContainer));
            using (FileStream fs = File.Open(pathToFile, FileMode.Create))
            {
                s.WriteObject(fs, sc);
            }
        }

        public ModelNodePrototype deserialize()
        {
            DataContractSerializer s = new DataContractSerializer(typeof(SerContainer));
            using (FileStream fs = File.Open(pathToFile, FileMode.Open))
            {
                SerContainer sc = (SerContainer)s.ReadObject(fs);
                sc.restorePrototypes();
                return sc.root.Protoype;
            }
        }

        public void save(ModelNodePrototype assembly)
        {
            Debug.WriteLine("ser start");
            serialize(assembly);
            Debug.WriteLine("ser end");
        }

        public ModelNodePrototype load()
        {
            return deserialize();
        }
    }

}