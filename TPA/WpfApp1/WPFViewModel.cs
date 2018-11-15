using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;
using ViewModel.viewmodel;

namespace WpfApp1
{
    class WPFViewModel : ViewModelClass, INotifyPropertyChanged
    {
        public override void Browse()
        {
            OpenFileDialog test = new OpenFileDialog()
            {
                Filter = "Dynamic Library File(*.dll)| *.dll"
            };
            test.ShowDialog();
            if (test.FileName.Length == 0)
                MessageBox.Show("No files selected");
            else
            {
                pathVariable = test.FileName;
                RaisePropertyChanged(nameof(pathVariable));

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
        }
    }
}
