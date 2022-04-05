using System;
using System.Xml.Serialization;

namespace ExchangeOfCurrencies.Test.LogicLayerTests.Model
{
    public class TestCurrency
    {
        [XmlElement("ParentCode")]
        public string ID { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("EngName")]
        public string EngName { get; set; }

        [XmlIgnore]
        public uint NumCode { get; set; }

        [XmlElement("ISO_Num_Code")]
        public string NumCodeAsString
        {
            get => NumCode.ToString();
            set
            {
                if (string.IsNullOrEmpty(value))
                    NumCode = 0;
                else
                    NumCode = Convert.ToUInt32(value);
            }
        }

        [XmlIgnore]
        public string CharCode { get; set; }

        [XmlElement("ISO_Char_Code")]
        public string CharCodeAsString
        {
            get => CharCode;
            set
            {
                if (string.IsNullOrEmpty(value))
                    CharCode = string.Empty;
                else
                    CharCode = value;
            }
        }


        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is TestCurrency currency)
            {
                return ID.Equals(currency.ID);
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }
    }
}
