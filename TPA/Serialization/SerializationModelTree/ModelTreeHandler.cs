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
        public ModelTreeHandler(AssemblyMetaData assemblyMetaData)
        {
            loadedNodes = new List<ModelNode>();
            rootNode = new ModelNodeAssembly(null, assemblyMetaData);
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

        public static Reflection.ModelTree.ModelTreeHandler createModelTree(Serialization.SerializationModelTree.ModelTreeHandler tree)
        {
            Reflection.ModelTree.ModelTreeHandler modelTree = new Reflection.ModelTree.ModelTreeHandler();

            modelTree.rootNode = new Reflection.ModelTree.ModelNodeAssembly(null, tree.rootNode.Name);
            tree.rootNode.loadAll();
            modelTree.currentNode = modelTree.rootNode;
            TreeSeek(modelTree.rootNode, tree.rootNode);
            modelTree.Load();
            return modelTree;
        }

        private static void TreeSeek(Reflection.ModelTree.ModelNode myNode, Serialization.SerializationModelTree.ModelNode node)
        {
            Reflection.ModelTree.ModelNode newNode = null;
            node.loadAll();
            foreach (Serialization.SerializationModelTree.ModelNode child in node.allNodes)
            {
                if (child.TypeName == "Method")
                {
                    myNode.allNodes.Add(newNode = new Reflection.ModelTree.ModelNodeMethod(myNode, ((Serialization.SerializationModelTree.ModelNodeMethod)child).Name));
                }
                else if (child.TypeName == "Namespace")
                {
                    myNode.allNodes.Add(newNode = new Reflection.ModelTree.ModelNodeNamespace(myNode, ((Serialization.SerializationModelTree.ModelNodeNamespace)child).Name));
                }
                else if (child.TypeName == "Type" || child.TypeName == "Property"
                    || child.TypeName == "Interface" || child.TypeName == "Nested Type"
                    || child.TypeName == "Base Type" || child.TypeName == "Declaring Type"
                    || child.TypeName == "Return Type" || child.TypeName == "Parameter Type")
                {
                    myNode.allNodes.Add(newNode = new Reflection.ModelTree.ModelNodeType(myNode, ((Serialization.SerializationModelTree.ModelNodeType)child).Name, ((Serialization.SerializationModelTree.ModelNodeType)child).TypeName));
                }
                else
                {
                    return;
                }

                TreeSeek(newNode, child);
            }
        }

        public static Serialization.SerializationModelTree.ModelTreeHandler createModelTreeToSer(Reflection.ModelTree.ModelTreeHandler tree)
        {
            Serialization.SerializationModelTree.ModelTreeHandler modelTree = new Serialization.SerializationModelTree.ModelTreeHandler();

            modelTree.rootNode = new Serialization.SerializationModelTree.ModelNodeAssembly(null, tree.rootNode.Name);
            //tree.rootNode.loadAll();
            modelTree.currentNode = modelTree.rootNode;
            TreeSeekToSer(modelTree.rootNode, tree.rootNode);
            modelTree.Load();
            return modelTree;
        }

        private static void TreeSeekToSer(Serialization.SerializationModelTree.ModelNode myNode, Reflection.ModelTree.ModelNode node)
        {
            Serialization.SerializationModelTree.ModelNode newNode = null;
            node.loadAll();
            foreach (Reflection.ModelTree.ModelNode child in node.allNodes)
            {
                if (child.TypeName == "Method")
                {
                    myNode.allNodes.Add(newNode = new Serialization.SerializationModelTree.ModelNodeMethod(myNode, ((Reflection.ModelTree.ModelNodeMethod)child).Name));
                }
                else if (child.TypeName == "Namespace")
                {
                    myNode.allNodes.Add(newNode = new Serialization.SerializationModelTree.ModelNodeNamespace(myNode, ((Reflection.ModelTree.ModelNodeNamespace)child).Name));
                }
                else if (child.TypeName == "Type" || child.TypeName == "Property"
                    || child.TypeName == "Interface" || child.TypeName == "Nested Type"
                    || child.TypeName == "Base Type" || child.TypeName == "Declaring Type"
                    || child.TypeName == "Return Type" || child.TypeName == "Parameter Type")
                {
                    myNode.allNodes.Add(newNode = new Serialization.SerializationModelTree.ModelNodeType(myNode, ((Reflection.ModelTree.ModelNodeType)child).Name, ((Reflection.ModelTree.ModelNodeType)child).TypeName));
                }
                else
                {
                    return;
                }

                TreeSeekToSer(newNode, child);
            }
        }

    }
}
