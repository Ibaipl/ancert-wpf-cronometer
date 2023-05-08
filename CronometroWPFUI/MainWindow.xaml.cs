using AncertCronometro.Domain;
using AncertCronometro.ViewModel;
using System.Windows;

namespace CronometroWPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CronometroViewModel(new Cronometro());
        }
    }
}
