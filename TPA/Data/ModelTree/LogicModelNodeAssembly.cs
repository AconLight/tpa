using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Reflection.Model;

namespace Data.ModelTree
{
    public class LogicModelNodeAssembly: LogicModelNode
    {
        public ObservableCollection<LogicModelNodeNamespace> Nodes { get; set; }

        public AssemblyMetaData assembly;

        public LogicModelNodeAssembly(LogicModelNode parent, AssemblyMetaData assembly): base(parent)
        {
            Nodes = new ObservableCollection<LogicModelNodeNamespace>();
            Name = assembly.Name;
            TypeName = "Assembly";
            this.assembly = assembly;
        }
        public LogicModelNodeAssembly(LogicModelNode parent, String Name) : base(parent)
        {
            Nodes = new ObservableCollection<LogicModelNodeNamespace>();
            this.Name = Name;
            TypeName = "Assembly";
        }

        public override void Load(List<LogicModelNode> loadedNodes)
        {
            if (assembly != null)
            foreach (NamespaceMetaData n in assembly.Namespaces)
            {
                Nodes.Add(new LogicModelNodeNamespace(this, n));
                Nodes.Last().tryLoad(loadedNodes);
            }
            loadAll();
            Console.WriteLine("siema " + Nodes.Count + " " + allNodes.Count);
        }
        public override void loadAll()
        {
            allNodes = new ObservableCollection<LogicModelNode>();
            foreach (LogicModelNode node in Nodes)
            {
                allNodes.Add(node);
            }
        }

        public override void retriveNode()
        {

        }

        public override void loadNodes()
        {
            Nodes = new ObservableCollection<LogicModelNodeNamespace>();
            foreach (LogicModelNode node in allNodes)
            {
                Nodes.Add((LogicModelNodeNamespace) node);
            }
            
        }
    }
}
