using System;
using System.Collections.Generic;

using ExchangeOfCurrencies.Test.LogicLayerTests.Model;

namespace ExchangeOfCurrencies.Test.LogicLayerTests.Helpers
{
    internal class CurrencyRateHelper
    {
        public static List<TestCurrencyRate> GetRates()
        {
            return new List<TestCurrencyRate>()
            {
                new TestCurrencyRate() { Date = "01.04.2022", Value = 83.4097M },
                new TestCurrencyRate() { Date = "02.04.2022", Value = 83.4285M },
                new TestCurrencyRate() { Date = "05.04.2022", Value = 83.5932M }
            };
        }
    }
}
