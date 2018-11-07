using Reflection.Model;
using Reflection.ModelTree;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class TreeViewItem
    {
        public ObservableCollection<TreeViewItem> Children { get; set; }
        public TreeViewItem myself { get; set; }
        public ModelNode whereAmI { get; set; }
        public string Name { get; set; }
        private bool m_IsExpanded;
        private bool m_WasBuilt;
        public Assembly assembly { get; set; }
        public AssemblyMetaData assemblyMetadata { get; set; }
        public ModelTreeHandler tree { get; set; }

        public TreeViewItem(ModelNode me, String name)
        {
            Name = name;
            Children = new ObservableCollection<TreeViewItem>();
            foreach (var child in me.nodes)
            {
                Children.Add(new TreeViewItem(child, "(" + child.TypeName + ") " + child.Name));
            }
            this.m_WasBuilt = false;
            whereAmI = me;
            
        }
        public TreeViewItem(String s)
        {
            Children = new ObservableCollection<TreeViewItem>();

            assembly = Assembly.LoadFrom(s);
            assemblyMetadata = new AssemblyMetaData(assembly);
            tree = new ModelTreeHandler(assemblyMetadata);
            tree.Load();
            this.m_WasBuilt = false;
            Name = "(" + tree.currentNode.TypeName + ") " + tree.currentNode.Name;
            whereAmI = tree.currentNode;

        }
        public bool IsExpanded
        {
            get { return m_IsExpanded; }
            set
            {
                m_IsExpanded = value;
                if (m_WasBuilt)
                    return;
                BuildMyself();

                m_WasBuilt = true;
            }
        }
        public void BuildMyself()
        {
            foreach(var child in whereAmI.nodes)
            {
                Children.Add(new TreeViewItem(child, "(" + child.TypeName + ") " + child.Name));
                child.Load();
            }
        }
        
    }
}
