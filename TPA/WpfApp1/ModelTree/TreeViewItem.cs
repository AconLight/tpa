using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ModelTree;

namespace WpfApp1.ModelTree
{
    class TreeViewItem
    {
        public ModelNode Node;
        public string Name { get; set; }
        public ObservableCollection<TreeViewItem> Children { get; set; }

        public Boolean IsExpanded { get; set; }

        public bool IsExpanded2
        {
            get { return Node.IsExpanded; }
            set
            {
                Node.Load();
                /*Children = new ObservableCollection<TreeViewItem>();
                foreach (ModelNode m in Node.Nodes)
                {
                    Children.Add(new TreeViewItem(m));
                    m.Load();
                }*/
            }
        }


        public  TreeViewItem(ModelNode Node)
        {
            this.Node = Node;
            Children = new ObservableCollection<TreeViewItem>();
            Name = Node.Name;
            IsExpanded = Node.IsExpanded;
        }
    }
}
