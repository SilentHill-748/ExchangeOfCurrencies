using System.Linq;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using ExchangeOfCurrencies.DbClient;

namespace ExchangeOfCurrencies.ClientModel.Validation
{
    public class LoginAttribute : ValidationAttribute
    {
        private const string loginsQuary = "SELECT login FROM users";

        public override bool IsValid(object value)
        {
            string login = (string)value;
            if (!login.Any(c => !char.IsLetter(c) && 
                                         !char.IsDigit(c)))
            {
                string xmlResponse = LoadLogins();
                if (CheckLoginToList(login, xmlResponse))
                {
                    ErrorMessage = "Данный логин уже занят!";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                ErrorMessage = "Указан неверный формат логина!";
                return false;
            }
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
            return Request.Send(loginsQuary).GetXml();
        }
    }
}
