using System.Collections.Generic;
using System.Xml.Serialization;

namespace ExchangeOfCurrencies.Test.LogicLayerTests.Model
{
    public class ValCurs
    {
        [XmlElement("ValCurs")]
        public List<TestCurrencyRate> Rates { get; set; }
    }
}
