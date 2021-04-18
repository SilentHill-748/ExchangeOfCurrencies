using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ExchangeOfCurrencies.DbClient;

namespace ExchangeOfCurrencies.UI.Windows
{
    public partial class TestMainWindowUI : Window
    {
        private List<Currency> allCurrencies;

        public TestMainWindowUI()
        {
            InitializeComponent();
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

        }

        private void SaleLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Init()
        {
            ListOfCurrencies.Background = Brushes.Black;
            allCurrencies = new List<Currency>();
            GetAllCurrencies();
            FillListOfCurrencies();
        }

        private void GetAllCurrencies()
        {
            DataTable tableOfCurrencies = Request.Send("SELECT * FROM currencies;").Tables[0];
            for (int i = 0; i < tableOfCurrencies.Rows.Count; i++)
                allCurrencies.Add(new Currency(tableOfCurrencies.Rows[i].ItemArray));
        }

        private void FillListOfCurrencies()
        {
            foreach (Currency currency in allCurrencies)
                ListOfCurrencies.Items.Add(currency.Name);
        }

        private void LoadInfoAboutChosenCurrency(Currency currency)
        {
            string[] currencyFields = {"Код","Сим. код","Название","Курс","Продажа","Количество" };
            for (int i = 0; i < currencyFields.Length; i++)
            {
                if (currencyFields[i].Length < 10)
                    InfoCurrentCurrency.Text += $"{currencyFields[i]+":",-11} {currency[i]}\n";
                else
                    InfoCurrentCurrency.Text += $"{currencyFields[i]}: {currency[i]}\n";
            }
        }
    }
}
