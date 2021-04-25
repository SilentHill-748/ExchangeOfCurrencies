using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly CultureInfo formatCulture;

        public User()
        {
            Log = new Logger();
            string currentDate = DateTime.Now.ToShortDateString();
            Log.Write($"Сессия начата в {currentTime}. Дата: {currentDate}.");
            formatCulture = new CultureInfo("en-US");
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
            string sumEngFormat = (balance + sum).ToString(formatCulture);
            Request.Send($"UPDATE user_wallet SET balance = {sumEngFormat:F2} WHERE userId = {UserId};");
            Log.Write($"{currentTime}: Пользователь \'{FirstName}\' пополнил баланс на {sum} руб.");
        }

        public void PurchaseCurrency(ref Currency currency, uint count)
        {
            decimal balance = GetActualValue("Balance");
            uint userCurrencyCount = Convert.ToUInt32(GetActualValue(currency.CharCode));
            decimal price = (decimal)(count * currency.Course);

            if (price > balance)
            {
                throw new Exception("На счету недостаточно средств!");
            }
            else
            {
                if (currency.Count < count)
                {
                    throw new Exception("В банке нет такого числа валюты!");
                }
                balance -= price;
                userCurrencyCount += count;
                currency.Count -= count;
                Update(currency, userCurrencyCount, balance);
            }
            Log.Write($"{currentTime}: Пользователь \'{FirstName}\' купил валюту {currency.CharCode}" +
                $" в количестве {count} ед. Сумма покупки составила {price:F2} руб.");
        }

        public void SellCurrency(ref Currency currency, uint count)
        {
            decimal balance = GetActualValue("Balance");
            decimal price = (decimal)(count * currency.Sale);
            uint userCurrencyCount = Convert.ToUInt32(GetActualValue(currency.CharCode));

            if (count > userCurrencyCount)
            {
                throw new Exception($"Недостаточное количество \'{currency.Name}\' для продажи!");
            }
            else
            {
                if (userCurrencyCount < count)
                {
                    throw new Exception("Вы не можете продать больше, чем у Вас есть!");
                }
                userCurrencyCount -= count;
                currency.Count += count;
                balance += price;
                Update(currency, userCurrencyCount, balance);
            }
            Log.Write($"{currentTime}: Пользователь \'{FirstName}\' продал валюту {currency.CharCode}" +
                $" в количестве {userCurrencyCount} ед. Сумма продажи составила {price:F2} руб.");
        }

        public decimal GetActualValue(string field)
        {
            string quary = $"SELECT {field} FROM user_wallet WHERE userId = {UserId};";
            DataTable result = Request.Send(quary).Tables[0];
            return decimal.Parse(result.Rows[0].ItemArray[0].ToString());
        }

        private void Update(Currency currency, uint userCurrencyCount, decimal userBalance)
        {
            string balance = userBalance.ToString(formatCulture);
            Request.Send($"UPDATE user_wallet SET {currency.CharCode} = {userCurrencyCount}, " +
                $"Balance = {balance} WHERE userId = {UserId};");
            Request.Send($"UPDATE currencies SET count = {currency.Count} WHERE charCode = \'{currency.CharCode}\'");
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
