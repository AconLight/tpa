using Reflection.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.ModelTree
{

    public class LogicModelNodeNamespace : LogicModelNode
    {
        public NamespaceMetaData m_namespace;

        public ObservableCollection<LogicModelNodeType> Nodes { get; set; }
        public LogicModelNodeNamespace(LogicModelNode parent, NamespaceMetaData m_namespace) : base(parent)
        {
            Nodes = new ObservableCollection<LogicModelNodeType>();
            Name = m_namespace.Name;
            TypeName = "Namespace";
            this.m_namespace = m_namespace;
        }

        public LogicModelNodeNamespace(LogicModelNode parent, String Name) : base(parent)
        {
            this.Name = Name;
            TypeName = "Namespace";
        }

        public override void Load(List<LogicModelNode> loadedNodes)
        {
            foreach (TypeMetaData t in m_namespace.Types)
            {
                if (t != null)
                {
                    Nodes.Add(new LogicModelNodeType(this, t, "Type"));
                    Nodes.Last().tryLoad(loadedNodes);
                }
            }
            loadAll();
        }
        public override void loadAll()
        {
            allNodes = new ObservableCollection<LogicModelNode>();
            foreach (LogicModelNode node in Nodes)
            {
                allNodes.Add(node);
            }
        }

        public override void loadNodes()
        {
            Nodes = new ObservableCollection<LogicModelNodeType>();
            foreach (LogicModelNode node in allNodes)
            {
                Nodes.Add((LogicModelNodeType)node);
            }
        }
    }
}
