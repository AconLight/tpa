using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reflection.Model;

namespace ViewModel.ModelTree
{
    public class ViewModelNodeAssembly: ViewModelNode
    {
        public AssemblyMetaData assembly;
        public ViewModelNodeAssembly(ViewModelNode parent, AssemblyMetaData assembly): base(parent)
        {
            Name = assembly.Name;
            TypeName = "Assembly";
            this.assembly = assembly;
        }
        public ViewModelNodeAssembly(ViewModelNode parent, String Name) : base(parent)
        {
            this.Name = Name;
            TypeName = "Assembly";
        }

        public override void Load()
        {
            IsExpanded = true;
            if (assembly != null)
                if (Nodes.Count() == 0)
                foreach (NamespaceMetaData n in assembly.Namespaces)
            {
                Nodes.Add(new ViewModelNodeNamespace(this, n));
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
