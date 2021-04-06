using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ExchangeOfCurrencies.ClientModel.Validation;
using ExchangeOfCurrencies.DbClient;
using ExchangeOfCurrencies.UI.Windows;

namespace ExchangeOfCurrencies.UI
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class AutorizationWindow : Window
    {
        private MainWindow mainWindow;    // Главное окно, ИМХО.
        private Registration regWindow;

        private string login = "";
        private string password = "";

        public AutorizationWindow()
        {
            InitializeComponent();
            ControlValidation validation = new();

            CurrencyUpdater updater = new();
            updater.Update();
        }

        #region Events
        private void RegistrationL_MouseDown(object sender, MouseButtonEventArgs e)
        {
            regWindow = new Registration();
            regWindow.Show();
            this.Close();
        }

        // Главная логика авторизации.
        private void AutorizationL_MouseUp(object sender, MouseButtonEventArgs e)
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

        private void Head_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ForgotPassL_MouseUp(object sender, MouseButtonEventArgs e)
        {
            string message = "Если Вы забыли свои входные данные, то обратитесь к своему системному администратору за помощью!";
            MessageBox.Show(message, "Справка",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CloseBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void PassBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) AutorizationClient();
        }
        #endregion

        private async void AutorizationClient()
        {
            try
            {
                HeaderLabel.Content += " | загрузка..";
                Autorization autorization = new(login, password);
                await Task.Run(() => autorization.BeginAutorization());                         // Начал проверку данных по БД.

                mainWindow = new MainWindow("Никита");
                mainWindow.Show();                                                  // Если ошибок нет - открываю основное окно и закрываю текущее.
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Внимание!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                HeaderLabel.Content = "Авторизация";
            }
        }
    }
}
