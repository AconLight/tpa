﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Reflection.Model;
using Reflection.ModelTree;

namespace Serialization.SerializationModelTree
{
    [DataContract(IsReference = true)]
    public class SerModelTreeHandler
    {
        private List<SerModelNode> loadedNodes;
        public SerModelNode currentNode;
        [DataMember]
        public SerModelNodeAssembly rootNode;

        public SerModelTreeHandler(Assembly assembly)
        {
            loadedNodes = new List<SerModelNode>();
            rootNode = new SerModelNodeAssembly(null, new AssemblyMetaData(assembly));
            currentNode = rootNode;
            Load();
        }
        public SerModelTreeHandler(AssemblyMetaData assemblyMetaData)
        {
            loadedNodes = new List<SerModelNode>();
            rootNode = new SerModelNodeAssembly(null, assemblyMetaData);
            currentNode = rootNode;
            Load();
        }
        public SerModelTreeHandler()
        {
            loadedNodes = new List<SerModelNode>();
        }
        public void Load()
        {
            currentNode.tryLoad(loadedNodes);
        }

        public void retriveModel()
        {

        }

        public static LogicModelTreeHandler createLogicModelTree(SerModelTreeHandler tree)
        {
            LogicModelTreeHandler modelTree = new LogicModelTreeHandler();

            modelTree.rootNode = new LogicModelNodeAssembly(null, tree.rootNode.Name);
            tree.rootNode.loadAll();
            modelTree.currentNode = modelTree.rootNode;
            TreeSeek(modelTree.rootNode, tree.rootNode);
            modelTree.Load();
            return modelTree;
        }

        private static void TreeSeek(LogicModelNode myNode, SerModelNode node)
        {
            LogicModelNode newNode = null;
            node.loadAll();
            foreach (SerModelNode child in node.allNodes)
            {
                if (child.TypeName == "Method")
                {
                    myNode.allNodes.Add(newNode = new LogicModelNodeMethod(myNode, ((SerModelNodeMethod)child).Name));
                }
                else if (child.TypeName == "Namespace")
                {
                    myNode.allNodes.Add(newNode = new LogicModelNodeNamespace(myNode, ((SerModelNodeNamespace)child).Name));
                }
                else if (child.TypeName == "Type" || child.TypeName == "Property"
                    || child.TypeName == "Interface" || child.TypeName == "Nested Type"
                    || child.TypeName == "Base Type" || child.TypeName == "Declaring Type"
                    || child.TypeName == "Return Type" || child.TypeName == "Parameter Type")
                {
                    myNode.allNodes.Add(newNode = new LogicModelNodeType(myNode, ((SerModelNodeType)child).Name, ((SerModelNodeType)child).TypeName));
                }
                else
                {
                    return;
                }
                myNode.loadNodes();
                if (!node.isLooped)
                    TreeSeek(newNode, child);
            }
        }

        public static SerModelTreeHandler createSerModelTreeToSer(LogicModelTreeHandler tree)
        {
            SerModelTreeHandler modelTree = new SerModelTreeHandler();

            modelTree.rootNode = new SerModelNodeAssembly(null, tree.rootNode.Name);
            Console.WriteLine("super debugowanie sera");
            Console.WriteLine(tree.rootNode.allNodes.Count);
            Console.WriteLine(tree.rootNode.Nodes.Count);
            //tree.rootNode.loadAll();
            modelTree.currentNode = modelTree.rootNode;
            List<SerModelNode> loadedNodes = new List<SerModelNode>();

            TreeSeekToSer(modelTree.rootNode, tree.rootNode, loadedNodes);
            modelTree.Load();
            return modelTree;
        }

        private static void TreeSeekToSer(SerModelNode myNode, LogicModelNode node, List<SerModelNode> loadedNodes)
        {
            Console.WriteLine(node.allNodes.Count);
            SerModelNode newNode = null;
            //node.loadAll();
            foreach (LogicModelNode child in node.allNodes)
            {
                if (child.TypeName == "Method")
                {
                    myNode.allNodes.Add(newNode = new SerModelNodeMethod(myNode, ((LogicModelNodeMethod)child).Name));
                }
                else if (child.TypeName == "Namespace")
                {
                    myNode.allNodes.Add(newNode = new SerModelNodeNamespace(myNode, ((LogicModelNodeNamespace)child).Name));
                }
                else if (child.TypeName == "Type" || child.TypeName == "Property"
                    || child.TypeName == "Interface" || child.TypeName == "Nested Type"
                    || child.TypeName == "Base Type" || child.TypeName == "Declaring Type"
                    || child.TypeName == "Return Type" || child.TypeName == "Parameter Type")
                {
                    myNode.allNodes.Add(newNode = new SerModelNodeType(myNode, ((LogicModelNodeType)child).Name, ((LogicModelNodeType)child).TypeName));
                }
                else
                {
                    return;
                }

                if (node.isLooped)
                {
                    foreach (SerModelNode vn in loadedNodes)
                    {
                        if (vn.TypeName == node.TypeName && vn.Name == node.Name)
                        {

                            myNode.allNodes.Clear();
                            foreach (SerModelNode vn2 in vn.allNodes)
                            {
                                myNode.allNodes.Add(vn2);
                            }
                            myNode.isLooped = true;
                            break;
                        }
                    }
                    myNode.loadNodes();
                    loadedNodes.Add(myNode);
                    continue;
                }
                myNode.loadNodes();
                loadedNodes.Add(myNode);
                TreeSeekToSer(newNode, child, loadedNodes);
            }
        }

    }
}