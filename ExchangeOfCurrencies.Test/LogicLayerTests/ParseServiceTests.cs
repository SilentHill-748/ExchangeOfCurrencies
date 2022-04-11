using NUnit.Framework;

using ExchangeOfCurrencies.Logic.Services;
using ExchangeOfCurrencies.Test.LogicLayerTests.Model;
using ExchangeOfCurrencies.Test.LogicLayerTests.Helpers;

namespace ExchangeOfCurrencies.Test.LogicLayerTests
{
    public class ParseServiceTests
    {
        [Test]
        public void ParseToCurrencyListTest()
        {
            var testCurrencies = CurrencyHelper.GetCurrencies();
            var parser = new ParseService<Valuta>();

            var res = (Valuta)parser.ParseAsync("https://www.cbr.ru/scripts/XML_valFull.asp").Result;

            Assert.NotNull(res);

            CollectionAssert.AllItemsAreUnique(res.TestCurrencies);

            res.TestCurrencies.ForEach(x => x.ID = x.ID.Replace(" ", ""));

            for (int i = 0; i < testCurrencies.Count; i++)
                Assert.True(res.TestCurrencies.Contains(testCurrencies[i]));
        }

        [Test]
        public void ParseToCurrencyRateListTest()
        {
            var rates = CurrencyRateHelper.GetRates();
            var parser = new ParseService<ValCurs>();

            var res = (ValCurs)parser.ParseAsync("https://www.cbr.ru/scripts/XML_dynamic.asp?date_req1=01/04/2022&date_req2=05/04/2022&VAL_NM_RQ=R01235").Result;

            Assert.NotNull(res);
            CollectionAssert.AllItemsAreUnique(res.Rates);

            for (int i = 0; i < res.Rates.Count; i++)
                Assert.Contains(rates[i], res.Rates);
        }
    }
}
