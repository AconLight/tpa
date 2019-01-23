using System.Windows;
using ViewModel.viewmodel;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewModelClass = new ViewModelClass(new BrowseElement());
            DataContext = new ViewModelClass(new BrowseElement());
            Closing += viewModelClass.OnWindowClosing;

        }
    }
}
