using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeOfCurrencies.ClientModel
{
    public class User : Person
    {
        private Dictionary<string, object> personalData;
        // TODO: Описать клиента.
        public User(Dictionary<string, object> personalData)
        {
            this.personalData = personalData;
            Init();
        }

        public void BuyCurrency()
        {

        }

        public void SellCurrency()
        {

        }

        private void Init()
        {
            SetPropertyValues();
        }

        private void SetPropertyValues()
        {
            PropertyInfo[] properties = this.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                if (!personalData.ContainsKey(properties[i].Name))
                    throw new Exception("Ошибка в целостности данных!");
                var setMethod = properties[i].SetMethod;
                object _ = setMethod.Invoke(this, new object[] { personalData[properties[i].Name] });
            }
        }
    }
}
