using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reflection.Model;

namespace ViewModel.ModelTree
{
    class ModelNodeAssembly: ModelNode
    {
        public AssemblyMetaData assembly;
        public ModelNodeAssembly(ModelNode parent, AssemblyMetaData assembly): base(parent)
        {
            Name = assembly.Name;
            TypeName = "Assembly";
            this.assembly = assembly;
        }

        public override void Load()
        {
            IsExpanded = true;
            foreach (NamespaceMetaData n in assembly.Namespaces)
            {
                Nodes.Add(new ModelNodeNamespace(this, n));
            }
        }
    }
}
