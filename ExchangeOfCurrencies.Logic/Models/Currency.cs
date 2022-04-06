using System.Xml.Serialization;

namespace ExchangeOfCurrencies.Logic.Models
{
    public class Currency
    {
        public Currency()
        {
            ID = string.Empty;
            Name = string.Empty;
            EngName = string.Empty;
            ParentCode = string.Empty;
        }


        [XmlAttribute("ID")]
        public string ID { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("EngName")]
        public string EngName { get; set; }

        [XmlElement("ParentCode")]
        public string ParentCode
        {
            get => ParentCode;
            set => ParentCode = value.Replace(" ", "");
        }

        [XmlIgnore]
        public uint NumCode { get; set; }

        [XmlElement("ISO_Num_Code")]
        public string StringNumCode
        {
            get => NumCode.ToString();
            set => NumCode = uint.TryParse(value, out uint result) ? result : 0;
        }

        [XmlElement("ISO_Char_Code")]
        public string CharCode
        {
            get => CharCode;
            set => CharCode = string.IsNullOrEmpty(value) ? string.Empty : value;
        }
    }
}
