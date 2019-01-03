using System;
using System.Collections.Generic;
using System.Reflection;
using Reflection.Model;

namespace Reflection.ModelTree
{
    public class ModelTreeHandler
    {
        private List<ModelNode> loadedNodes;
        public ModelNode currentNode;
        public ModelNodeAssembly rootNode;
        DataBridgeInterface dbi;
        public ModelTreeHandler(Assembly assembly)
        {
            loadedNodes = new List<ModelNode>();
            rootNode = new ModelNodeAssembly(null, new AssemblyMetaData(assembly));
            currentNode = rootNode;
            Load();
        }
        public ModelTreeHandler(DataBridgeInterface dbi)
        {
            this.dbi = dbi;
            loadedNodes = new List<ModelNode>();
            rootNode = dbi.read();
            currentNode = rootNode;
        }
        public ModelTreeHandler(AssemblyMetaData assemblyMetaData)
        {
            loadedNodes = new List<ModelNode>();
            rootNode = new ModelNodeAssembly(null, assemblyMetaData);
            currentNode = rootNode;
            Load();
        }
        public ModelTreeHandler(ModelNodeAssembly modelNodeAssembly)
        {
            loadedNodes = new List<ModelNode>();
            rootNode = modelNodeAssembly;
            currentNode = rootNode;
            Load();
        }
        public ModelTreeHandler()
        {
        }
        public void Load()
        {
            currentNode.tryLoad(loadedNodes);
        }

        public void retriveModel()
        {

        }

    }
}