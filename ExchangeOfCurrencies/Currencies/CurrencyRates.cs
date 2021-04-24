using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data;
using System.Net;

namespace ExchangeOfCurrencies.Currencies
{
    /// <summary>
    ///     Объект, обновляющий все валюты в БД по курсу.
    /// </summary>
    public class CurrencyRates
    {
        private string URL;
        private readonly List<Currency> currencies;

        public CurrencyRates()
        {
            DateTime now = DateTime.Now;
            string dateNow = $"{now:dd}/{now:MM}/{now:yyyy}";
            URL = $"http://www.cbr.ru/scripts/XML_daily.asp?date_req={dateNow}";
            currencies = new();
            GetListOfCurrencies();
        }

        /// <summary>
        ///     Обновляет все курсы валют в БД на актуальные курсы в день запуска программы.
        /// </summary>
        public void Update()
        {
            List<double> newRates = GetListNewValuesFromXml(GetNewExchangeRates()); // Получить список актуальных курсов.
            List<double> newSaleRates = GetListSaleValues(newRates);                // Установить курс продажи в 95% от актуальных курсов.

            string updateQuary = BuildUpdateQuary(newRates, newSaleRates);
            Request.Send(updateQuary);                                              // Отправка запроса в БД.
        }

        // Строит строку запроса из предоставленных данных.
        private string BuildUpdateQuary(List<double> rates, List<double> saleRates)
        {
            string updateQuary = "";
            var engCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");

            for (int i = 0; i < rates.Count; i++)
            {
                string course = rates[i].ToString(engCulture);
                string saleCourse = saleRates[i].ToString(engCulture);
                updateQuary += $"UPDATE currencies SET course = {course}, sale = {saleCourse} " +
                    $"WHERE charCode = \'{currencies[i].CharCode}\';";
            }
            return updateQuary;
        }

        private List<double> GetListSaleValues(List<double> rates)
        {
            return rates.Select(m => Math.Round((m * 0.95), 4)).ToList();
        }

        private List<double> GetListNewValuesFromXml(string newRates)
        {
            List<double> rates = new();

            string pattern = @$"<CharCode>(.*?)</CharCode>(.*?)<Value>(.*?)</Value>";
            MatchCollection matches = Regex.Matches(newRates, pattern);

            for (int i = 0; i < currencies.Count; i++)
                for (int k = 0; k < matches.Count; k++)
                {
                    if (matches[k].Groups[1].Value == currencies[i].CharCode)
                    {
                        rates.Add(double.Parse(matches[k].Groups[3].Value));
                        break;
                    }
                }
            return rates;
        }

        // Возвращает список с новыми курсами для каждой валюты из списка валют.
        private string GetNewExchangeRates()
        {
            using WebClient client = new();
            return client.DownloadString(URL);
        }

        // Выгружает все валюты из БД.
        private void GetListOfCurrencies()
        {
            Request getCurrencies = new("SELECT * FROM currencies");

            var rows = getCurrencies.DataSet.Tables[0].Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                Currency currency = new(rows[i].ItemArray);
                currencies.Add(currency);
            }
        }
    }
}
