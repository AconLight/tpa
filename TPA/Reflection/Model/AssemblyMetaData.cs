using Composition;
using ModelTransfer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;


namespace Reflection.Model
{
    public class AssemblyMetaData: ModelNodePrototype
    {
        public AssemblyMetaData(Assembly assembly)
        {
            m_Name = assembly.ManifestModule.Name;
            m_Namespaces = from Type _type in assembly.GetTypes()
                           //where _type.GetVisible()
                           group _type by _type.GetNamespace() into _group
                           orderby _group.Key
                           select new NamespaceMetaData(_group.Key, _group);
        }

        public AssemblyMetaData(ModelNodePrototype prot)
        {
            Name = prot.Name;
            TypeName = "Assembly";
            Mods = "";
            Nodes = prot.Nodes;
        }


        private string m_Name;
        private IEnumerable<NamespaceMetaData> m_Namespaces;
        public IEnumerable<NamespaceMetaData> Namespaces { get => m_Namespaces; set => m_Namespaces = value; }


        public override void OnCreate()
        {
            Name = m_Name;
            TypeName = "Assembly";
            Mods = "";
        }
        public override void OnLoad()
        {
            Nodes = new List<ModelNodePrototype>();
            foreach(ModelNodePrototype prot in m_Namespaces)
            {
                Nodes.Add(prot);
            }
        }

        public void Save()
        {
            MEF.getContainer().GetExportedValue<DataBridgeInterface>(ConfigurationManager.AppSettings["DataSerOpt"]).save(this);
        }

        public void Load()
        {
            ModelNodePrototype root = MEF.getContainer().GetExportedValue<DataBridgeInterface>(ConfigurationManager.AppSettings["DataSerOpt"]).load();
            Name = root.Name;
            TypeName = root.TypeName;
            Mods = root.Mods;
            Parent = root.Parent;
            Nodes = root.Nodes;
        }
    }
}