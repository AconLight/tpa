using Reflection.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ModelTree
{
    class ViewModelNodeNamespace : ViewModelNode
    {
        public NamespaceMetaData m_namespace;
        public ViewModelNodeNamespace(ViewModelNode parent, NamespaceMetaData m_namespace) : base(parent)
        {
            Name = m_namespace.Name;
            TypeName = "Namespace";
            this.m_namespace = m_namespace;
        }

        public ViewModelNodeNamespace(ViewModelNode parent, String Name) : base(parent)
        {
            this.Name = Name;
            TypeName = "Namespace";
        }

        public override void Load()
        {
            IsExpanded = true;
            if(Nodes.Count() == 0 && m_namespace != null)
            foreach (TypeMetaData t in m_namespace.Types)
            {
                if (t != null) Nodes.Add(new ViewModelNodeType(this, t, "Type"));
            }
        }

        public override void loadAll()
        {
            allNodes = new ObservableCollection<ViewModelNode>();
            foreach (ViewModelNode node in Nodes)
            {
                allNodes.Add(node);
            }
        }
    }
}
