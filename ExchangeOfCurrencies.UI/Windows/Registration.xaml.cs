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

using ExchangeOfCurrencies.ClientModel;
using ExchangeOfCurrencies.DbClient;

namespace ExchangeOfCurrencies.UI.Windows
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        User regData;

        public Registration()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            regData = new User();
        }

        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AutorizationWindow loginWin = new();
            loginWin.Show();
            this.Close();
        }

        private void CancelL_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CloseBox_MouseDown(sender, e);
        }

        private void RegL_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ShowErrors(string[] errors)
        {
            string message = string.Join("\n", errors);
            MessageBox.Show(message, "Внимание!", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        //TODO: ВЫНЕСТИ НА УРОВЕНЬ КЛАССА Client.cs!
        // Проверка ввода данных.
        //private void Validate(Client client)
        //{
        //    var context = new Validation.ValidationContext(client);
        //    var errors = new List<Validation.ValidationResult>();
        //    bool isValidated = Validation.Validator.TryValidateObject(client, context, errors, true);

        //    if (!isValidated)
        //    {
        //        string errorMessage = ShowValidateErrors(errors);
        //        throw new Validation.ValidationException(errorMessage);
        //    }
        //}

        //private string ShowValidateErrors(List<Validation.ValidationResult> errors)
        //{
        //    string errorMessage = "";
        //    foreach (Validation.ValidationResult error in errors)
        //    {
        //        errorMessage += error.ErrorMessage + "\n";
        //    }
        //    return errorMessage;
        //}
    }
}
