using System;
using System.Reflection;

namespace ExchangeOfCurrencies.Currencies
{
    public class Currency
    {
        private readonly object[] items;
        private PropertyInfo[] properties;

        public Currency(object[] items)
        {
            this.items = items;
            Init();
        }

        public int NumCode { get; set; }
        public string CharCode { get; set; }
        public string Name { get; set; }
        public double Course { get; set; }
        public double Sale { get; set; }
        public uint Count { get; set; }

        public object this[int index]
        {
            get
            {
                if (index >= items.Length || index < 0)
                {
                    throw new Exception();
                }
                return properties[index].GetValue(this);
            }
        }

        private void Init()
        {
            properties = this.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                Type propertyType = properties[i].PropertyType;
                switch (propertyType.Name)
                {
                    case "Int32":
                        properties[i].SetValue(this, Convert.ToInt32(items[i]));
                        break;
                    case "Double":
                        properties[i].SetValue(this, Convert.ToDouble(items[i]));
                        break;
                    case "UInt32":
                        properties[i].SetValue(this, Convert.ToUInt32(items[i]));
                        break;
                    case "String":
                        properties[i].SetValue(this, items[i].ToString());
                        break;
                }
            }
        }
    }
}
