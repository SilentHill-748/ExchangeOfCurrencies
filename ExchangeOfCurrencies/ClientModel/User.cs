using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Data;

using ExchangeOfCurrencies.DbClient;
using ExchangeOfCurrencies.Currencies;

namespace ExchangeOfCurrencies.ClientModel
{
    public class User : Person
    {
        private readonly List<string> registrationData;

        public User() { }

        public User(List<string> registrationPersonalData)
        {
            registrationData = registrationPersonalData;
            SetRegistrationPropertyValues();
        }

        /// <summary>
        /// Обновляет данные в БД в поле Balance на число sum пользователя.
        /// </summary>
        /// <param name="sum"></param>
        public void TopUpBalance(decimal sum)
        {
            decimal balance = GetActualBalance();
            CultureInfo culture = new("en-US");
            string sumEngFormat = (balance + sum).ToString(culture);
            Request.Send($"UPDATE user_wallet SET balance = {sumEngFormat:F2} WHERE userId = {UserId};");
        }

        public void BuyCurrency(Currency currency, decimal count)
        {
            
        }

        public void SellCurrency(Currency currency, decimal count)
        {
            throw new InvalidOperationException("Данной валюта в кошельке отсутствует!");
        }

        private decimal GetActualBalance()
        {
            string quary = $"SELECT balance FROM user_wallet WHERE userId = {UserId};";
            DataTable table = Request.Send(quary).Tables[0];
            return decimal.Parse(table.Rows[0].ItemArray[0].ToString());
        }

        private void SetRegistrationPropertyValues()
        {
            var properties = this.GetType().GetProperties().
                Where(p => p.Name != "UserId").ToArray();
            if (registrationData.Count != properties.Length)
                throw new Exception("Указаны не все регистрационные данные!");

            for (int i = 0; i < properties.Length; i++)
                properties[i].SetValue(this, registrationData[i]);
        }
    }
}
