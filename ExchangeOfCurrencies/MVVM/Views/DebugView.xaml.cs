using System.Windows;

namespace ExchangeOfCurrencies.MVVM.Views
{
    public partial class DebugView : Window
    {
        public DebugView()
        {
            InitializeComponent();
        }

        private void ShowAutorizViewBtn_Click(object sender, RoutedEventArgs e)
        {
            new AutorizationView().Show();
        }

        private void ShowRegViewBtn_Click(object sender, RoutedEventArgs e)
        {
            new RegistrationView().Show();
        }

        private void ShowMainViewBtn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
        }
    }
}
