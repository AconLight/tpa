using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Reflection.Model;

namespace Serialization.SerializationModelTree
{
    [DataContract(IsReference = true)]
    public class ModelTreeHandler
    {
        private List<ModelNode> loadedNodes;
        public ModelNode currentNode;
        [DataMember]
        public ModelNodeAssembly rootNode;

        public ModelTreeHandler(Assembly assembly)
        {
            loadedNodes = new List<ModelNode>();
            rootNode = new ModelNodeAssembly(null, new AssemblyMetaData(assembly));
            currentNode = rootNode;
            Load();
        }
        public void Load()
        {
            currentNode.tryLoad(loadedNodes);
        }
    }

}
