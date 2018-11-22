using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel;

namespace WpfApp1
{
    class BrowseElement : IBrowse
    {
        public string Browse()
        {
            OpenFileDialog test = new OpenFileDialog()
            {
                Filter = "Dynamic Library File(*.dll)| *.dll"
            };
            test.ShowDialog();
            if (test.FileName.Length == 0)
            {
                MessageBox.Show("No files selected");
                return null;
            } 
            else
            {
                return test.FileName;
                //pathVariable = test.FileName;
                //RaisePropertyChanged(nameof(pathVariable));

            }
        }
    }
}
