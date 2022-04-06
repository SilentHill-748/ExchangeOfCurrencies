using System.Xml.Serialization;

namespace ExchangeOfCurrencies.Logic.Models
{
    public class CurrencyRate
    {
        public CurrencyRate()
        {
            ID = string.Empty;
        }


        [XmlAttribute("Id")]
        public string ID { get; set; }

        [XmlIgnore]
        public DateOnly Date { get; set; }

        [XmlAttribute("Date")]
        public string StringDate
        {
            get => Date.ToShortDateString();
            set => Date = DateOnly.TryParse(value, out DateOnly date) ? 
                date : 
                DateOnly.MinValue;
        }

        [XmlElement("Value")]
        public decimal Value { get; set; }
    }
}
