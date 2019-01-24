using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelTransfer;

namespace ViewModel.ModelTree
{
    public class ViewModelNode: ModelNode
    {
        public string TypeName { get; set; }
        public string Name { get; set; }
        public string Mods { get; set; }
        public ObservableCollection<ViewModelNode> MyNodes { get; set; }
        public Boolean IsExpanded { get; set; }

        public bool OpenClose
        {
            get { return IsExpanded; }
            set
            {
                if (value)
                {
                    IsExpanded = true;
                    Load();
                }
                else { IsExpanded = false; }
            }
        }

        public ViewModelNode(ModelNode node) : base(node.Parent, node.Protoype)
        {
            MyNodes = new ObservableCollection<ViewModelNode>();
            OnCreate();
        }

        private ViewModelNode(ModelNode Parent, ModelNodePrototype Protoype): base(Parent, Protoype)
        {
            MyNodes = new ObservableCollection<ViewModelNode>();
            OnCreate();
        }

        public override void OnCreate()
        {
            Name = Protoype.Mods + " " + Protoype.TypeName + " " + Protoype.Name;
            TypeName = Protoype.TypeName;
            Mods = Protoype.Mods;
        }

        public override void OnLoad()
        {
            MyNodes.Clear();
            foreach (ModelNode n in Nodes)
            {
                ViewModelNode vn = new ViewModelNode(this, n.Protoype);
                MyNodes.Add(vn);
            }
        }
    }
}
