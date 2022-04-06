using System;
using System.Xml.Serialization;

namespace ExchangeOfCurrencies.Test.LogicLayerTests.Model
{
    public class TestCurrencyRate
    {
        [XmlAttribute("Date")]
        public string Date { get; set; }

        [XmlIgnore]
        public int Nominal { get; set; }

        [XmlElement("Value")]
        public decimal Value { get; set; }


        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is TestCurrencyRate rate)
            {
                return rate.Value == Value;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}
