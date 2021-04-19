using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace ExchangeOfCurrencies.ClientModel.Validation
{
    public class EmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string email = value as string;
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[A-Za-z0-9]{2,17}))$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
