using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeOfCurrencies.DbClient
{
    public class Currency
    {
        private readonly object[] items;
        public int NumCode => (int)items[0];
        public string CharCode => (string)items[1];
        public string Name => (string)items[2];
        public double Course => (double)items[3];
        public double Sale => (double)items[4];
        public int Count => (int)items[5];

        public object this[int index]
        {
            get
            {
                if (index >= items.Length || index < 0)
                    throw new Exception();
                return items[index];
            }
        }

        public Currency(object[] items)
        {
            this.items = items;
        }
    }
}
