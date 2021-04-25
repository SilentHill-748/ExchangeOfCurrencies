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
using ExchangeOfCurrencies.Logging;

namespace ExchangeOfCurrencies.ClientModel
{
    public class User : Person
    {
        private readonly List<string> registrationData;
        private readonly string currentTime = DateTime.Now.ToShortTimeString();

        public User() { }

        public User(List<string> registrationPersonalData)
        {
            registrationData = registrationPersonalData;
            SetRegistrationPropertyValues();
            Log = new Logger();
            Log.Write($"Сессия начата в {currentTime}. Дата: {DateTime.Now.Date}.");
        }

        /// <summary>
        /// Представляет поток данных о всех действиях пользователя.
        /// </summary>
        public Logger Log { get; }

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
            Log.Write($"{currentTime}: Пользователь \'{FirstName}\' пополнил баланс на {sum} руб.");
        }

        public void BuyCurrency(Currency currency, uint count)
        {
            decimal purchaiseSum = 0;
            Log.Write($"{currentTime}: Пользователь \'{FirstName}\' купил валюту {currency.CharCode}" +
                $" в количестве {count} ед. Сумма покупки составила {purchaiseSum} руб.");
        }

        public void SellCurrency(Currency currency, uint count)
        {
            decimal sellSum = 0;
            Log.Write($"{currentTime}: Пользователь \'{FirstName}\' продал валюту {currency.CharCode}" +
                $" в количестве {count} ед. Сумма продажи составила {sellSum} руб.");
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
