using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using ExchangeOfCurrencies.DbClient;

namespace ExchangeOfCurrencies.ClientModel.Validation
{
    public class LoginAttribute : ValidationAttribute
    {
        private const string loginsQuary = "SELECT login FROM employees";

        public override bool IsValid(object value)
        {
            string login = (string)value;
            string xmlRequest = LoadLogins();
            return CheckLoginToList(login, xmlRequest);
        }

        // Проверка нового логина в списке имеющихся.
        private bool CheckLoginToList(string login, string xmlData)
        {
            string pattern = $@"<login>{login}</login>";
            return Regex.IsMatch(xmlData, pattern);
        }

        // Вытаскиваю логины из БД. Метод вернет xml-документ.
        private string LoadLogins()
        {
            Request logins = new(loginsQuary);
            return logins.DataSet.GetXml();
        }
    }
}
