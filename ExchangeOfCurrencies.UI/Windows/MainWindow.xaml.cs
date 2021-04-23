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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

using ExchangeOfCurrencies.ClientModel;
using ExchangeOfCurrencies.DbClient;

namespace ExchangeOfCurrencies.UI
{
    public partial class MainWindow : Window
    {
        private readonly User currentUser;
        private List<Currency> allCurrencies;
        private string selectBalance;
        private string selectInfoAboutCurrenciesOfUser;

        public MainWindow(User currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            Init();
        }

        #region Events
        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void CloseBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void ListOfCurrencies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListOfCurrencies.SelectedIndex == -1) return;

            InfoCurrentCurrency.Text = "";
            string currencyName = ListOfCurrencies.SelectedItem.ToString();
            Currency currency = allCurrencies.Find(m => m.Name.Equals(currencyName));
            LoadInfoAboutChosenCurrency(currency);
        }

        private void PurchaseLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UpdateListOfCurrencies();
        }

        private void SaleLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UpdateListOfCurrencies();
        }

        private void GetReportLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        private void Init()
        {
            WelcomMessage.Content = $"Привет, {currentUser.FirstName}! :)";
            selectBalance = $"SELECT Balance FROM user_wallet WHERE userId = {currentUser.UserId};";
            selectInfoAboutCurrenciesOfUser = $"SELECT USD, EUR, CAD, CNY, BYN, DKK, SGD " +
                $"FROM user_wallet WHERE userId = {currentUser.UserId};";
            allCurrencies = new List<Currency>();
            GetAllCurrencies();
            FillListOfCurrencies();
            FillInfoAbouCurrentUser();
        }

        private void FillInfoAbouCurrentUser()
        {
            decimal balance = GetBalance();
            string[] userCurrencies = GetUserCurrencies();
            BalanceInfo.Content = $"На Вашем счете: {balance:F2} руб.";
            PurchasedCurrencies.Text = "Купленные валюты:\n" + string.Join("\n", userCurrencies);
        }

        private decimal GetBalance()
        {
            // По запросу гарантируется результат в виде всего 1 значения единственного поля Balance.
            DataTable selectResult = Request.Send(selectBalance).Tables[0];
            object balance = selectResult.Rows[0].ItemArray[0];
            return Convert.ToDecimal(balance);
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
            string[] currencyFields = { "Код:", "Символьный код:", "Название:", "Курс:", "Продажа:", "Количество:" };
            int maxLength = currencyFields.Max(m => m.Length);
            for (int i = 0; i < currencyFields.Length; i++)
            {
                string currentCurrencyInfo = currencyFields[i].PadRight(maxLength);
                InfoCurrentCurrency.Text += $"{currentCurrencyInfo} {currency[i]}\n";
            }
        }

        // Асинхронное обновление списка доступных валют.
        private async void UpdateListOfCurrencies()
        {
            allCurrencies.Clear();
            ListOfCurrencies.Items.Clear();
            await Task.Run(() => GetAllCurrencies());
        }

        // todolist
        // TODO: Реализовать систему запросов в бд для покупки и продажи валют.
        // TODO: Реализовать систему логирования действий юзера и алгоритм составления отчета.
    }
}
