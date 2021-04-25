using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using ExchangeOfCurrencies.ClientModel;

namespace ExchangeOfCurrencies.UI.Windows.MessageWindows
{
    public partial class TopUpBalance : Window
    {
        private readonly string defaultText = "Введите сумму...";
        private readonly User currentUser;

        public TopUpBalance(User currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
        }

        #region Events
        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void SumText_GotFocus(object sender, RoutedEventArgs e)
        {
            SumText.Text = "";
        }

        private void SumText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SumText.Text.Length == 0)
            {
                SumText.Text = defaultText;
            }
        }

        private void SumText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((SumText.Text == "") || 
                (SumText.Text == defaultText))
            {
                return;
            } 
            if (decimal.TryParse(SumText.Text, out decimal sum))
            {
                SwitchErrorLabel("", true);
            }
            else
            {
                SwitchErrorLabel("Некорректный ввод! Пример: 123,56", false);
            }
        }

        private void TopUpLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            decimal sum = Convert.ToDecimal(SumText.Text);
            currentUser.TopUpBalance(sum);
            Close();
        }
        #endregion

        #region Private
        private void SwitchErrorLabel(string content, bool isEnable)
        {
            ErrorLabel.Content = content;
            TopUpLabel.IsEnabled = isEnable;
        }
        #endregion
    }
}
