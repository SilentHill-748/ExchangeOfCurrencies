using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using LiveCharts;
using LiveCharts.Wpf;
using System.Xml.Linq;

namespace ExchangeOfCurrencies.Currencies
{
    /// <summary>
    /// Реализует линию графика валюты в диапазоне дат.
    /// </summary>
    public class CurrencyChartLine
    {
        private readonly Currency currentCurrency;
        private readonly int rangeOfDays;

        public CurrencyChartLine(Currency currency, int rangeOfDays)
        {
            currentCurrency = currency;
            this.rangeOfDays = rangeOfDays;
        }

        /// <summary>
        /// Вернёт линию графика с заданным значением котировок валюты в диапазоне дней, 
        /// указанного при инициализации объекта <see cref="CurrencyChartLine"/>.
        /// </summary>
        public LineSeries GetLine()
        {
            string[] values = GetActualData();
            LineSeries line = new();
            line.Values = new ChartValues<string>(values);
            line.StrokeThickness = 2;
            return line;
        }

        private string[] GetActualData()
        {
            string rateQuotes = GetXmlRateQuotes();
            return GetRateQuotes(rateQuotes).Select(rate => $"{rate:F4}").ToArray();
        }

        private string GetXmlFromCBR(string link)
        {
            WebClient parser = new();
            return parser.DownloadString(link);
        }

        private string GetXmlRateQuotes()
        {
            DateTime today = DateTime.Today;
            string startDate = GetStartDate();
            string endDate = $"{today:dd}/{today:MM}/{today:yyyy}";
            string valuteID = GetValuteIDFromXml().First();

            return GetXmlFromCBR($"http://www.cbr.ru/scripts/XML_dynamic.asp?date_req1={startDate}" +
                $"&date_req2={endDate}&VAL_NM_RQ={valuteID}");
        }

        private IEnumerable<string> GetValuteIDFromXml()
        {
            string currencyInfo = GetXmlFromCBR("http://www.cbr.ru/scripts/XML_daily.asp");
            var values = from valute in XDocument.Load(currencyInfo).Elements()
                         from node in valute.Elements()
                         where node.Value == currentCurrency.CharCode
                         select node.Value;
            if (!values.Any())
                throw new Exception("Такой валюты не существует!");
            return values;
        }

        private IEnumerable<double> GetRateQuotes(string xml)
        {
            return from record in XDocument.Load(xml).Elements()
                   from node in record.Elements()
                   where node.Name == "Value"
                   select Convert.ToDouble(node.Value);
        }

        // rangeAtDays - диапазон дней, от начальной даты до сегодня. 14 - диапазон в 2 недели от сегодня.
        private string GetStartDate()
        {
            DateTime date = DateTime.Today - TimeSpan.FromDays(rangeOfDays);
            return $"{date:dd}/{date:MM}/{date:yyyy}";
        }
    }
}
