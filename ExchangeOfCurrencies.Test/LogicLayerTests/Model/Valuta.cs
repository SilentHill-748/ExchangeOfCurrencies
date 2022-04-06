using System.Collections.Generic;
using System.Xml.Serialization;

namespace ExchangeOfCurrencies.Test.LogicLayerTests.Model
{
    public class Valuta
    {
        [XmlElement("Item")]
        public List<TestCurrency> TestCurrencies { get; set; }
    }
}
