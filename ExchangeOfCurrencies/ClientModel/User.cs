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
        private readonly string currentTime = DateTime.Now.ToLongTimeString();

        public User()
        {
            Log = new Logger();
            string currentDate = DateTime.Now.ToShortDateString();
            Log.Write($"Сессия начата в {currentTime}. Дата: {currentDate}.");
        }

        public User(List<string> registrationPersonalData) : this()
        {
            registrationData = registrationPersonalData;
            SetRegistrationPropertyValues();
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
            decimal balance = GetActualValue("Balance");
            CultureInfo culture = new("en-US");
            string sumEngFormat = (balance + sum).ToString(culture);
            Request.Send($"UPDATE user_wallet SET balance = {sumEngFormat:F2} WHERE userId = {UserId};");
            Log.Write($"{currentTime}: Пользователь \'{FirstName}\' пополнил баланс на {sum} руб.");
        }

        public void BuyCurrency(Currency currency, uint count)
        {
            decimal balance = GetActualValue("Balance");
            uint currentCount = Convert.ToUInt32(GetActualValue(currency.CharCode));
            decimal price = (decimal)(count * currency.Course);

            if (price > balance)
            {
                throw new Exception("На счету недостаточно средств!");
            }
            else
            {
                balance -= price;
                currentCount += count;
                Request.Send($"UPDATE user_wallet SET {currency.CharCode} = {currentCount}, " +
                    $"Balance = {balance} WHERE userId = {UserId};");
            }
            Log.Write($"{currentTime}: Пользователь \'{FirstName}\' купил валюту {currency.CharCode}" +
                $" в количестве {count} ед. Сумма покупки составила {price} руб.");
        }

        public void SellCurrency(Currency currency, uint count)
        {
            decimal balance = GetActualValue("Balance");
            decimal price = (decimal)(count * currency.Sale);
            uint currentCount = Convert.ToUInt32(GetActualValue(currency.CharCode));

            if (count > currentCount)
            {
                throw new Exception($"Недостаточное количество \'{currency.Name}\' для продажи!");
            }
            else
            {
                currentCount -= count;
                balance += price;
                Request.Send($"UPDATE user_wallet SET {currency.CharCode} = {currentCount}, " +
                    $"Balance = {balance} WHERE userId = {UserId};");
            }

            Log.Write($"{currentTime}: Пользователь \'{FirstName}\' продал валюту {currency.CharCode}" +
                $" в количестве {0} ед. Сумма продажи составила {price} руб.");
        }

        private decimal GetActualValue(string field)
        {
            string quary = $"SELECT {field} FROM user_wallet WHERE userId = {UserId};";
            DataTable table = Request.Send(quary).Tables[0];
            return decimal.Parse(table.Rows[0].ItemArray[0].ToString());
        }

        private void SetRegistrationPropertyValues()
        {
            var properties = (from property in this.GetType().GetProperties()
                              where property.Name != "UserId" && property.Name != "Log"
                              select property).ToArray();
            if (registrationData.Count != properties.Length)
                throw new Exception("Указаны не все регистрационные данные!");

            for (int i = 0; i < properties.Length; i++)
                properties[i].SetValue(this, registrationData[i]);
        }
    }
}
