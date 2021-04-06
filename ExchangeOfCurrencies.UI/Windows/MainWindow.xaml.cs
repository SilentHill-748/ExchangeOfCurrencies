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
using ExchangeOfCurrencies.DbClient;

namespace ExchangeOfCurrencies.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string loadData = "SELECT currencies.numCode, charCode, name, course, sale, count FROM currencies";
        private string login;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Init()
        {
            Request getData = new (loadData);
            dataGrid.ItemsSource = FillTable(getData.DataSet);
            dataGrid.RowHeight = 20;
        }

        public MainWindow(string login)
        {
            InitializeComponent();
            this.login = login;
            Init();
        }

        private void CloseBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private List<Currency> FillTable(DataSet selectedTable)
        {
            List<Currency> currencies = new List<Currency>();
            var table = selectedTable.Tables[0];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                Currency currency = new Currency(table.Rows[i].ItemArray);
                currencies.Add(currency);
            }
            return currencies;
        }

        // todolist
        // TODO: Реализовать систему обновления БД новыми курсами валют из бд.
        // TODO: Реализовать систему запросов в бд для покупки и продажи валют.
        // TODO: Реализовать систему логирования действий юзера и алгоритм составления отчета.
        // TODO: Написать модуль авторизации и регистрации по БД.
        // TODO: Связать все системы в одно целое приложение, сохраняя составленный дизайн.
    }

    // TODO: Решить вопрос с классом Currency!
    public class Currency
    {
        private readonly object[] items;
        public int NumCode => (int)items[0];
        public string CharCode => (string)items[1];
        public string Name => (string)items[2];
        public double Course => (double)items[3];
        public double Sale => (double)items[4];
        public int Count => (int)items[5];

        public Currency(object[] items)
        {
            this.items = items;
        }
    }
}
