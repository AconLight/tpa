using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelTransfer;

namespace ViewModel.ModelTree
{
    public class ViewModelNode: ModelNode
    {
        public string TypeName;
        public string Name { get; set; }
        public ObservableCollection<ViewModelNode> MyNodes { get; set; }
        public Boolean IsExpanded { get; set; }

        public bool OpenClose
        {
            get { return IsExpanded; }
            set
            {
                if (value)
                {
                    Load();
                }
                else { IsExpanded = false; }
            }
        }

        public ViewModelNode(ModelNode Parent, ModelNodePrototype Protoype): base(Parent, Protoype)
        {
            OnCreate();
        }

        protected override void OnCreate()
        {
            Name = Protoype.Name;
            TypeName = Protoype.TypeName;
        }

        protected override void OnLoad()
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
