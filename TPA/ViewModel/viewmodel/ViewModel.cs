using ViewModel.ModelTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ViewModel.viewmodel
{
    public class ViewModel
    {
        public ModelTreeHandler Tree;
        public ViewModelInterface ViewModelInterface;

        public void SetInterface(ViewModelInterface ViewModelInterface)
        {
            this.ViewModelInterface = ViewModelInterface;
        }

        public void LoadTree(Assembly assembly)
        {
            Tree = new ModelTreeHandler(assembly);
        }

    }
}
