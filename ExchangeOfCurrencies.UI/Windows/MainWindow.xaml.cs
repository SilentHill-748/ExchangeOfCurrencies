using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using System.Net.Mail;
using System.Net;

using ExchangeOfCurrencies.ClientModel;
using ExchangeOfCurrencies.Currencies;
using ExchangeOfCurrencies.DbClient;
using ExchangeOfCurrencies.UI.Windows.MessageWindows;

namespace ExchangeOfCurrencies.UI
{
    public partial class MainWindow : Window
    {
        private readonly User currentUser;
        private List<Currency> allCurrencies;
        private string selectInfoAboutCurrenciesOfUser;
        private Currency selectedCurrency;

        public MainWindow(User currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            Init();
        }

        #region Events
        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            currentUser.Log.Dispose();
            Environment.Exit(Environment.ExitCode);
        }

        private void ListOfCurrencies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string currencyName = ListOfCurrencies.SelectedItem.ToString();
            Currency currency = allCurrencies.Find(m => m.Name.Equals(currencyName));
            selectedCurrency = currency;
            LoadInfoAboutChosenCurrency(currency);
            InitCartesianChartLine(currency);
        }

        private void TopUpBalanceLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TopUpBalance topUpBalanceWindow = new(currentUser);
            topUpBalanceWindow.ShowDialog();
            UpdateInfoAbouCurrentUser();
        }

        private void PurchaseLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!uint.TryParse(CountOfCurrencyText.Text, out uint count))
                {
                    throw new Exception("Не указано количество покупаемой валюты!");
                }

                currentUser.PurchaseCurrency(ref selectedCurrency, count);
                UpdateAll();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message, "Упс..");
            }
        }

        private void SaleLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!uint.TryParse(CountOfCurrencyText.Text, out uint count))
                {
                    throw new Exception("Не указано количество продаваемой валюты!");
                }

                currentUser.SellCurrency(ref selectedCurrency, count);
                UpdateAll();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message, "Упс..");
            }
        }

        private void GetReportLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Message message = new("Отчёт отправлен на Ваш email.", "Внимание!");
            message.ShowDialog();
            SendMail();
        }
        #endregion

        #region Private
        private void Init()
        {
            WelcomMessage.Content = $"Привет, {currentUser.FirstName}! :)";
            selectInfoAboutCurrenciesOfUser = $"SELECT USD, EUR, CAD, CNY, BYN, DKK, SGD " +
                $"FROM user_wallet WHERE userId = {currentUser.UserId};";
            allCurrencies = new List<Currency>();
            GetAllCurrencies();
            FillListOfCurrencies();
            UpdateInfoAbouCurrentUser();
        }

        private void UpdateInfoAbouCurrentUser()
        {
            decimal balance = currentUser.GetActualValue("Balance");
            string[] userCurrencies = GetUserCurrencies();
            BalanceInfo.Content = $"На Вашем счёте: {balance:F2} руб.";
            PurchasedCurrencies.Text = "Купленные валюты:\n" + string.Join("\n", userCurrencies);
        }

        private void UpdateAll()
        {
            UpdateInfoAbouCurrentUser();
            LoadInfoAboutChosenCurrency(selectedCurrency);
            CountOfCurrencyText.Text = "";
        }

        private string[] GetUserCurrencies()
        {
            DataTable resultTable = Request.Send(selectInfoAboutCurrenciesOfUser).Tables[0];
            string[] infoAboutOfCurrencies = new string[resultTable.Columns.Count];
            for (int i = 0; i < resultTable.Columns.Count; i++)
            {
                string columnName = resultTable.Columns[i].ColumnName;
                string currencyInfo = $"{columnName} ({resultTable.Rows[0].ItemArray[i]})";
                infoAboutOfCurrencies[i] = currencyInfo;
            }
            return infoAboutOfCurrencies;
        }

        // Заполнение коллекции валют из БД.
        private void GetAllCurrencies()
        {
            DataTable tableOfCurrencies = Request.Send("SELECT * FROM currencies;").Tables[0];
            for (int i = 0; i < tableOfCurrencies.Rows.Count; i++)
                allCurrencies.Add(new Currency(tableOfCurrencies.Rows[i].ItemArray));
        }

        // Заполнение списка доступных валют.
        private void FillListOfCurrencies()
        {
            foreach (Currency currency in allCurrencies)
                ListOfCurrencies.Items.Add(currency.Name);
        }

        // Вывод всей информации по выбранной валюте.
        private void LoadInfoAboutChosenCurrency(Currency currency)
        {
            InfoCurrentCurrency.Text = "";
            string[] currencyFields = { "Код:", "Символьный код:", "Название:", "Курс:", "Продажа:", "Количество:" };
            int maxLength = currencyFields.Max(m => m.Length);
            for (int i = 0; i < currencyFields.Length; i++)
            {
                string currentCurrencyInfo = currencyFields[i].PadRight(maxLength);
                InfoCurrentCurrency.Text += $"{currentCurrencyInfo} {currency[i]}\n";
            }
        }

        private void UpdateCurrencies()
        {
            DataTable table = Request.Send("SELECT * FROM currencies;").Tables[0];
            for (int i = 0; i < allCurrencies.Count; i++)
            {
                Currency currency = new(table.Rows[i].ItemArray);
                allCurrencies[i] = currency;
            }
        }

        private void SendMail()
        {
            try
            {
                MailAddress sender = new("exchangesup02@gmail.com", "Support");
                MailAddress recipient = new(currentUser.Email);
                MailMessage mail = new(sender, recipient);
                mail.Subject = "Report";
                mail.Body = string.Join("\n", currentUser.Log.WriteAllLines());
                SmtpClient smtp = new("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("exchangesup02", "support748");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception e)
            {
                Message mes = new(e.Message, "Упс..");
                mes.ShowDialog();
            }
        }

        private void InitCartesianChartLine(Currency currency)
        {
            CurrencyChart.Visibility = Visibility.Visible;
            CurrencyChartLine line = new(currency, 10);
            var lineSeries = line.GetLine();
            CurrencyRateQuotes.Values = lineSeries.Values;
            CurrencyRateQuotes.Title = currency.CharCode;
            xAxis.Labels = line.Labels;
            xAxis.MaxValue = line.Labels.Count - 1;
            yAxis.MinValue = line.MinValue;
            yAxis.MaxValue = line.MaxValue;
        }

        private void ShowError(string message, string header)
        {
            Message mes = new(message, header);
            mes.ShowDialog();
        }
        #endregion
    }
}
