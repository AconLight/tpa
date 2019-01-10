using System;
using System.Collections.Generic;
using System.Reflection;
using Reflection.Model;

namespace Reflection.ModelTree
{
    public class LogicModelTreeHandler
    {
        private List<LogicModelNode> loadedNodes;
        public LogicModelNode currentNode;
        public LogicModelNodeAssembly rootNode;
        DataBridgeInterface dbi;
        public LogicModelTreeHandler(Assembly assembly)
        {
            loadedNodes = new List<LogicModelNode>();
            rootNode = new LogicModelNodeAssembly(null, new AssemblyMetaData(assembly));
            currentNode = rootNode;
        }
        public LogicModelTreeHandler(DataBridgeInterface dbi)
        {
            this.dbi = dbi;
            loadedNodes = new List<LogicModelNode>();
            //rootNode = dbi.read();
            currentNode = rootNode;
        }
        public LogicModelTreeHandler(AssemblyMetaData assemblyMetaData)
        {
            loadedNodes = new List<LogicModelNode>();
            rootNode = new LogicModelNodeAssembly(null, assemblyMetaData);
            currentNode = rootNode;
            Load();
        }
        public LogicModelTreeHandler(LogicModelNodeAssembly modelNodeAssembly)
        {
            loadedNodes = new List<LogicModelNode>();
            rootNode = modelNodeAssembly;
            currentNode = rootNode;
            Load();
        }
        public LogicModelTreeHandler()
        {
            loadedNodes = new List<LogicModelNode>();
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