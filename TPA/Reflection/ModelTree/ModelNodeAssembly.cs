using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reflection.Model;

namespace Reflection.ModelTree
{
    class ModelNodeAssembly: ModelNode
    {
        public AssemblyMetaData assembly;
        public ModelNodeAssembly(ModelNode parent, AssemblyMetaData assembly): base(parent)
        {
            Name = assembly.Name;
            this.assembly = assembly;
        }

        public override void Load()
        {
            Console.WriteLine("opened");
            isOpen = true;
            foreach (NamespaceMetaData n in assembly.Namespaces)
            {
                nodes.Add(new ModelNodeNamespace(this, n));
            }
        }
    }
}
