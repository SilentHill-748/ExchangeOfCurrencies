using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data;

namespace ExchangeOfCurrencies.DbClient
{
    public class Autorization
    {
        private const string getClientLoginAndPass = "SELECT login, password FROM clients";
        private KeyValuePair<string, string> userLoginPassPair;
        private Dictionary<string, string> loginPassPairs;

        public Autorization(string login, string password)
        {
            loginPassPairs = new();
            userLoginPassPair = new(login, password);
            FillLoginPassPairs();
        }

        public bool BeginAutorization()
        {
            foreach (var keyValuePair in loginPassPairs)
            {
                if (keyValuePair.Key == userLoginPassPair.Key)
                    if (keyValuePair.Value == userLoginPassPair.Value)
                        return true;
            }
            throw new Exception("Введен неверный логин или пароль!");
        }

        // Выгружает из БД все пары Логин/Пароль и возвращает XML-представление результата запроса.
        private string LoadData()
        {
            Request getData = new(getClientLoginAndPass);
            return getData.DataSet.GetXml();
        }

        // Заполняет словарь записей логин/пароль парами из XML-кода.
        private void FillLoginPassPairs()
        {
            string xmlData = LoadData();
            var logins = Regex.Matches(xmlData, @"<login>(.*?)</login>").Select(m => m.Groups[1].Value).ToList();
            var passwords = Regex.Matches(xmlData, @"<password>(.*?)</password>").Select(m => m.Groups[1].Value).ToList();

            for (int i = 0; i < logins.Count; i++)
                loginPassPairs.Add(logins[i], passwords[i]);
        }
    }
}