using System.IO;
using ViewModel.viewmodel;

namespace ConsoleApp1
{
    class BrowseConsole : ViewModel.IBrowse
    {
        public string Browse()
        {
            return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"TPA.ApplicationArchitecture.dll");
        }
    }
}
