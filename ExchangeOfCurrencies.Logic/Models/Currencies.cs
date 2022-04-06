using System.Xml.Serialization;

namespace ExchangeOfCurrencies.Logic.Models
{
    public class Currencies
    {
        [XmlElement("Item")]
        public List<Currency>? List { get; set; }
    }
}
