﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reflection.Model;

namespace ViewModel.ModelTree
{
    public class ModelTreeHandler
    {
        public ModelNode currentNode;
        public ModelNode rootNode;

        public ModelTreeHandler(AssemblyMetaData assemblyMetaData)
        {
            currentNode = new ModelNodeAssembly(null, assemblyMetaData);
            rootNode = currentNode;
            //Load();
        }

        public void GoToParent()
        {
            if (currentNode.Parent != null) currentNode = currentNode.Parent;
        }

        public void GoToChild(ModelNode node)
        {
            if (node != null)
            {
                currentNode = node;
            }
        }

        public void Load()
        {
            currentNode.Load();
        }

        public void Close()
        {
            currentNode.Close();
        }

        public ObservableCollection<ModelNode> getChildren()
        {
            return currentNode.Nodes;
        }

        public static List<ModelNode> TreePrint(ModelNode node)
        {
            List<ModelNode> nodes = new List<ModelNode>();
            foreach (ModelNode n in node.Nodes)
            {
                if (n.IsOpen)
                {
                    nodes.Add(n);
                }
            }

            return nodes;
        }
    }

}
