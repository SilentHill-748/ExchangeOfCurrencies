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
        private readonly string loadData = "SELECT * FROM currencies";
        private string login;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Init()
        {
            Request getData = new (loadData);
            currienciesTable.ItemsSource = FillTable(getData.DataSet);
            currienciesTable.RowHeight = 20;
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
            List<Currency> currencies = new();
            var table = selectedTable.Tables[0];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                Currency currency = new(table.Rows[i].ItemArray);
                currencies.Add(currency);
            }
            return currencies;
        }

        private void InitTable()
        {
            currienciesTable.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        }

        // todolist
        // TODO: Реализовать систему обновления БД новыми курсами валют из бд.
        // TODO: Реализовать систему запросов в бд для покупки и продажи валют.
        // TODO: Реализовать систему логирования действий юзера и алгоритм составления отчета.
        // TODO: Написать модуль авторизации и регистрации по БД.
        // TODO: Связать все системы в одно целое приложение, сохраняя составленный дизайн.
    }
}
