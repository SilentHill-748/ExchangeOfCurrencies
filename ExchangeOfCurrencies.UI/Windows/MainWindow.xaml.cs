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
        private User currentUser;
        private List<Currency> allCurrencies;

        public MainWindow(User currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            Init();
        }

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

        private void Init()
        {
            WelcomMessage.Content = $"Привет, {currentUser.FirstName}! :)";
            allCurrencies = new List<Currency>();
            GetAllCurrencies();
            FillListOfCurrencies();
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
            string[] currencyFields = { "Код", "Сим. код", "Название", "Курс", "Продажа", "Количество" };
            for (int i = 0; i < currencyFields.Length; i++)
            {
                if (currencyFields[i].Length < 10)
                    InfoCurrentCurrency.Text += $"{currencyFields[i] + ":",-11} {currency[i]}\n";
                else
                    InfoCurrentCurrency.Text += $"{currencyFields[i]}: {currency[i]}\n";
            }
        }

        // Асинхронное обновление списка доступных валют.
        private async void UpdateListOfCurrencies()
        {
            allCurrencies.Clear();
            ListOfCurrencies.Items.Clear();
            await Task.Run(() =>
            {
                GetAllCurrencies();
            });
        }

        // todolist
        // TODO: Реализовать систему запросов в бд для покупки и продажи валют.
        // TODO: Реализовать систему логирования действий юзера и алгоритм составления отчета.
    }
}
