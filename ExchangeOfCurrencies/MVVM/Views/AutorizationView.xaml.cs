using System.Windows;
using System.Windows.Controls;

using ExchangeOfCurrencies.Core;
using ExchangeOfCurrencies.MVVM.ViewModels;

namespace ExchangeOfCurrencies.MVVM.Views
{
    public partial class AutorizationView : Window, ICloseable
    {
        private readonly AutorizationViewModel _viewModel;


        public AutorizationView()
        {
            InitializeComponent();

            AutorizationViewModel vm = new();
            _viewModel = vm;

            this.DataContext = vm;
        }

        private void PasswordField_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.Password += ((PasswordBox)sender).Password;
        }

        private void ShowRegistrationWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            RegistrationView registration = new();
            registration.ShowDialog();
        }
    }
}
