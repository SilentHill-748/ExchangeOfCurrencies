using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.IO;

using LiveCharts;
using LiveCharts.Wpf;

namespace ExchangeOfCurrencies.Currencies
{
    /// <summary>
    /// Реализует линию графика валюты в диапазоне дат.
    /// </summary>
    public class CurrencyChartLine
    {
        private readonly Currency currentCurrency;
        private readonly int rangeOfDays;
        private string pathToTempFile;

        public CurrencyChartLine(Currency currency, int rangeOfDays)
        {
            currentCurrency = currency;
            this.rangeOfDays = rangeOfDays;
            pathToTempFile = "temp.xml";
        }

        public IList<string> Labels { get; private set; }
        public double MinValue { get; private set; }
        public double MaxValue { get; private set; }

        /// <summary>
        /// Вернёт линию графика с заданным значением котировок валюты в диапазоне дней, 
        /// указанного при инициализации объекта <see cref="CurrencyChartLine"/>.
        /// </summary>
        public LineSeries GetLine()
        {
            IEnumerable<double> values = GetActualData();
            LineSeries line = new();
            line.Values = new ChartValues<double>(values);
            line.StrokeThickness = 1;
            File.Delete(pathToTempFile);
            return line;
        }

        private IEnumerable<double> GetActualData()
        {
            WriteRateQuotesToFile();
            Labels = GetDatesFromRateQuotes(pathToTempFile);
            var values = GetRateQuotes(pathToTempFile);
            MinValue = (int)(values.Min() - 1);
            MaxValue = (int)(values.Max() + 1);
            return values;
        }

        private void WriteXmlToFile(string link)
        {
            string xml = GetXmlFrom(link);
            XmlDocument doc = new();
            doc.LoadXml(xml);
            doc.Save(pathToTempFile);
        }

        private void WriteRateQuotesToFile()
        {
            DateTime today = DateTime.Today;
            string startDate = GetStartDate();
            string endDate = $"{today:dd}/{today:MM}/{today:yyyy}";
            string valuteID = GetValuteIDFromXml().First();

            WriteXmlToFile($"http://www.cbr.ru/scripts/XML_dynamic.asp?date_req1={startDate}" +
                $"&date_req2={endDate}&VAL_NM_RQ={valuteID}");
        }

        private IEnumerable<string> GetValuteIDFromXml()
        {
            WriteXmlToFile("http://www.cbr.ru/scripts/XML_daily.asp");
            var values = from valCurs in XDocument.Load(pathToTempFile).Elements()
                         from valute in valCurs.Elements()
                         from node in valute.Elements()
                         where node.Value == currentCurrency.CharCode
                         select valute.Attribute("ID").Value;
            if (!values.Any())
            {
                throw new Exception("Такой валюты не существует!");
            }
            return values;
        }

        private IEnumerable<double> GetRateQuotes(string path)
        {
            var values = from valCurs in XDocument.Load(path).Elements()
                         from record in valCurs.Elements()
                         from node in record.Elements()
                         where node.Name == "Value"
                         select Convert.ToDouble(node.Value);
            return values.Select(value => Math.Round(value, 4));
        }

        private IList<string> GetDatesFromRateQuotes(string path)
        {
            var dates = from valCurs in XDocument.Load(path).Elements()
                        from record in valCurs.Elements()
                        select record.Attribute("Date").Value.Substring(0, 5);
            return dates.ToList();
        }

        // rangeAtDays - диапазон дней, от начальной даты до сегодня. 14 - диапазон в 2 недели от сегодня.
        private string GetStartDate()
        {
            DateTime date = DateTime.Today - TimeSpan.FromDays(rangeOfDays);
            return $"{date:dd}/{date:MM}/{date:yyyy}";
        }

        private string GetXmlFrom(string link)
        {
            WebClient parser = new();
            string xml = parser.DownloadString(link);
            return Regex.Replace(xml, "windows-1251", "utf-8");
        }
    }
}
