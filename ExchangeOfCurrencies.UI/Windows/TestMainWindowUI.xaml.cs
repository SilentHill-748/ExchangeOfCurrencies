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

using ExchangeOfCurrencies.DbClient;

namespace ExchangeOfCurrencies.UI.Windows
{
    public partial class TestMainWindowUI : Window
    {
        Currency Currency = new (new object[] { "840", "USD", "Dollar USA", "74.5755" ,"70.8467" ,"25000000" });
        public TestMainWindowUI()
        {
            InitializeComponent();
            PurchasedCurrencies.Text = "Купленные валюты:\n" +
                "Доллар США на сумму 12 USD\n" +
                "Доллар США на сумму 12 USD\n" +
                "Доллар США на сумму 12 USD\n" +
                "Доллар США на сумму 12 USD\n" +
                "USD: 12\n" +
                "USD на сумму 12\n" +
                "DSG на сумму 12 ед.\n";
            LoadInfoAboutChosenCurrency();
        }

        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void LoadInfoAboutChosenCurrency()
        {
            string[] currencyFields = {"Код","Сим. код","Название","Курс","Продажа","Количество" };
            for (int i = 0; i < currencyFields.Length; i++)
            {
                if (currencyFields[i].Length < 10)
                    InfoCurrentCurrency.Text += $"{currencyFields[i]+":",-11} {Currency[i]}\n";
                else
                    InfoCurrentCurrency.Text += $"{currencyFields[i]}: {Currency[i]}\n";
            }
        }
    }
}
