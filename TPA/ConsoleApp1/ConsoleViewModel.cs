using System.IO;
using ViewModel.viewmodel;

namespace ConsoleApp1
{
    class ConsoleViewModel : ViewModelClass
    {
        public override void Browse()
        {
            pathVariable = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"TPA.ApplicationArchitecture.dll");
        }
    }
}
