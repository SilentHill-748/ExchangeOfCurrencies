using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using ExchangeOfCurrencies.UI.Windows.MessageWindows;
using ExchangeOfCurrencies.DbClient;
using ExchangeOfCurrencies.UI.Windows;
using ExchangeOfCurrencies.ClientModel;
using ExchangeOfCurrencies.Currencies;

namespace ExchangeOfCurrencies.UI
{
    public partial class AutorizationWindow : Window
    {
        private MainWindow mainWindow;    // Главное окно, ИМХО.
        private Registration regWindow;

        private string login;
        private string password;

        public AutorizationWindow()
        {
            InitializeComponent();
            Init();
        }

        #region Events
        private void RegistrationL_MouseDown(object sender, MouseButtonEventArgs e)
        {
            regWindow = new Registration();
            regWindow.Show();
            Close();
        }

        private void AutorizationL_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AutorizationClient();
        }

        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            login = LoginBox.Text;
        }

        private void PassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            password = PassBox.Password;
        }

        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ForgotPassL_MouseUp(object sender, MouseButtonEventArgs e)
        {
            string message = "Если Вы забыли свои входные данные, то обратитесь к своему системному администратору за помощью!";
            Message error = new(message, "Забыл пароль!");
            error.ShowDialog();
        }

        private void CloseBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void PassBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) AutorizationClient();
        }
        #endregion

        private void Init()
        {
            BeginUpdateRowsToDb();
            login = string.Empty;
            password = string.Empty;
        }

        private void BeginUpdateRowsToDb()
        {
            Task.Run(() =>
            {
                CurrencyRates updater = new();
                updater.Update();
            });
        }

        private async void AutorizationClient()
        {
            try
            {
                HeaderLabel.Content += " | загрузка..";
                Autorization autorization = new ("SilentHill", "k20pqr3256qsh1");
                User currentUser = await Task.Run(() => autorization.BeginAutorization()); // Начал проверку данных по БД.

                mainWindow = new MainWindow(currentUser);
                mainWindow.Show();                                                  // Если ошибок нет - открываю основное окно и закрываю текущее.
                this.Close();
            }
            catch (Exception ex)
            {
                Message error = new(ex.Message, "Внимание!");
                error.ShowDialog();
            }
            finally
            {
                HeaderLabel.Content = "Авторизация";
            }
        }
    }
}
