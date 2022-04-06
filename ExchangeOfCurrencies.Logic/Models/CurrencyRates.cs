using System.Xml.Serialization;

namespace ExchangeOfCurrencies.Logic.Models
{
    public class CurrencyRates
    {
        [XmlElement("ValCurs")]
        public List<CurrencyRate>? Rates { get; set; }
    }
}
