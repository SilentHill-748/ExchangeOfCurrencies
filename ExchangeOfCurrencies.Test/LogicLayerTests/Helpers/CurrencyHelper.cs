using System.Collections.Generic;

using ExchangeOfCurrencies.Data.Entities;
using ExchangeOfCurrencies.Test.LogicLayerTests.Model;

namespace ExchangeOfCurrencies.Test.LogicLayerTests.Helpers
{
    internal class CurrencyHelper
    {
        public static TestCurrency GetCurrency()
        {
            return new TestCurrency()
            {
                ID = "R01010",
                Name = "Австралийский доллар",
                EngName = "Australian Dollar",
                NumCode = 36,
                CharCode = "AUD"
            };
        }

        public static List<TestCurrency> GetCurrencies()
        {
            return new List<TestCurrency>()
            {
                new TestCurrency()
                {
                    ID = "R01010",
                    Name = "Австралийский доллар",
                    EngName = "Australian Dollar",
                    NumCode = 36,
                    CharCode = "AUD"
                },
                new TestCurrency()
                {
                    ID = "R01015",
                    Name = "Австрийский шиллинг",
                    EngName = "Austrian Shilling",
                    NumCode = 40,
                    CharCode = "ATS"
                },
                new TestCurrency()
                {
                    ID = "R01020A",
                    Name = "Азербайджанский манат",
                    EngName = "Azerbaijan Manat",
                    NumCode = 944,
                    CharCode = "AZN"
                },
                new TestCurrency()
                {
                    ID = "R01035",
                    Name = "Фунт стерлингов Соединенного королевства",
                    EngName = "British Pound Sterling",
                    NumCode = 826,
                    CharCode = "GBR"
                }
            };
        }

        public static bool AreEqual(Currency currency, TestCurrency testCurrency)
        {
            return currency.ID.Equals(testCurrency.ID) &&
                currency.Name.Equals(testCurrency.Name) &&
                currency.EngName.Equals(testCurrency.EngName) &&
                currency.NumCode == testCurrency.NumCode &&
                currency.CharCode.Equals(testCurrency.CharCode);
        }
    }
}
