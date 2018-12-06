using System;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Runtime.Serialization;
using Serialization.SerializationModelTree;
using System.Reflection;

namespace Serialization
{
    public class Ser
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

        public void serialize()
        {
            ModelTreeHandler tree = new ModelTreeHandler(assembly);
            DataContractSerializer s = new DataContractSerializer(typeof(ModelTreeHandler));
            using (FileStream fs = File.Open(pathToFile, FileMode.Create))
            {
                s.WriteObject(fs, tree);
            }
        }

        public ModelTreeHandler deserialize()
        {
            DataContractSerializer s = new DataContractSerializer(typeof(ModelTreeHandler));
            using (FileStream fs = File.Open(pathToFile, FileMode.Open))
            {
                return (ModelTreeHandler)s.ReadObject(fs);
            }
        }
    }

}