using System.Linq;

using NUnit.Framework;

using ExchangeOfCurrencies.Data.Entities;
using ExchangeOfCurrencies.Logic.Interfaces;
using ExchangeOfCurrencies.Logic;
using ExchangeOfCurrencies.Test.LogicLayerTests.Helpers;
using ExchangeOfCurrencies.Test.LogicLayerTests.Model;

namespace ExchangeOfCurrencies.Test.LogicLayerTests
{
    public class ModelMapperTests
    {
        private readonly IModelMapper _mapper;


        public ModelMapperTests()
        {
            _mapper = new ModelMapper();
        }


        [Test]
        public void MapOneTestCurrencyToCurrencyEntityTest()
        {
            TestCurrency expectedCurrency = CurrencyHelper.GetCurrency();

            Currency actualCurrency = _mapper.Map<Currency, TestCurrency>(expectedCurrency);

            Assert.True(CurrencyHelper.AreEqual(actualCurrency, expectedCurrency));
        }

        [Test]
        public void MapCollectionOfTestCurrenciesToCollectionOfCurrencyEntityTest()
        {
            var listTestCurrincies = CurrencyHelper.GetCurrencies();

            var listCurrencies = _mapper.Map<Currency, TestCurrency>(listTestCurrincies).ToList();

            Assert.AreEqual(listCurrencies.Count, listTestCurrincies.Count);

            for (int i = 0; i < listTestCurrincies.Count; i++)
            {
                Assert.True(CurrencyHelper.AreEqual(
                    listCurrencies[i],
                    listTestCurrincies[i]));
            }
        }
    }
}
