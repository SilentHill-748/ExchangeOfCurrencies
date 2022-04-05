using System.Collections.Generic;

using ExchangeOfCurrencies.Test.LogicLayerTests.Model;

namespace ExchangeOfCurrencies.Test.LogicLayerTests.Helpers
{
    internal class CurrencyHelper
    {
        public static List<TestCurrency> GetCurrencies()
        {
            return new List<TestCurrency>()
            {
                new TestCurrency() { ID = "R01010    " },
                new TestCurrency() { ID = "R01015    " },
                new TestCurrency() { ID = "R01020    " },
                new TestCurrency() { ID = "R01035    " },
                new TestCurrency() { ID = "R01040    " },
                new TestCurrency() { ID = "R01060    " },
                new TestCurrency() { ID = "R01090    " },
                new TestCurrency() { ID = "R01095    " },
                new TestCurrency() { ID = "R01100    " },
                new TestCurrency() { ID = "R01115    " }
            };
        }
    }
}
