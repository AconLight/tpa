using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Reflection.Model;

namespace Serialization.SerializationModelTree
{
    [DataContract(IsReference = true)]
    public class ModelNodeAssembly: ModelNode
    {
        [DataMember]
        public ObservableCollection<ModelNodeNamespace> Nodes { get; set; }

        public AssemblyMetaData assembly;
        public ModelNodeAssembly(ModelNode parent, AssemblyMetaData assembly): base(parent)
        {
            Nodes = new ObservableCollection<ModelNodeNamespace>();
            Name = assembly.Name;
            TypeName = "Assembly";
            this.assembly = assembly;
        }

        public override void Load(List<ModelNode> loadedNodes)
        {
            foreach (NamespaceMetaData n in assembly.Namespaces)
            {
                Nodes.Add(new ModelNodeNamespace(this, n));
                Nodes.Last().tryLoad(loadedNodes);
            }
            
        }
        public override void loadAll()
        {
            allNodes = new ObservableCollection<ModelNode>();
            foreach(ModelNode node in Nodes)
            {
                allNodes.Add(node);
            }
        }
    }
}
