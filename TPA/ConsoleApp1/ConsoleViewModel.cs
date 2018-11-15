using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.viewmodel;

namespace ConsoleApp1
{
    class ConsoleViewModel : ViewModelClass
    {
        public override void Browse()
        {
            String p = Console.ReadLine();
            pathVariable = p;
        }
    }
}
