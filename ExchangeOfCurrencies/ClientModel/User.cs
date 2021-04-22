﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ExchangeOfCurrencies.DbClient;

namespace ExchangeOfCurrencies.ClientModel
{
    public class User : Person
    {
        private List<string> personalData;
        private Dictionary<Currency, decimal> wallet; // TODO: Допилить модель кошелька и покупки.

        public User(List<string> personalData)
        {
            this.personalData = personalData;
            SetPropertyValues();
        }

        public void BuyCurrency(Currency currency, decimal count)
        {
            if (wallet.ContainsKey(currency))
                wallet[currency] += count;
            else
                wallet.Add(currency, count);
        }

        public void SellCurrency(Currency currency, decimal count)
        {
            if (wallet.ContainsKey(currency))
                wallet[currency] -= wallet[currency] < count ?
                    throw new InvalidOperationException("Недостаточно средств!") : count;
            else
                throw new InvalidOperationException("Данной валюта в кошельке отсутствует!");
        }

        private void SetPropertyValues()
        {
            PropertyInfo[] properties = this.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                if (personalData.Count != properties.Length)
                    throw new Exception("Указаны не все регистрационные данные!");
                properties[i].SetValue(this, personalData[i]);
            }
        }
    }
}
