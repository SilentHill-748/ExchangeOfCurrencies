using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ExchangeOfCurrencies.ClientModel.Validation
{
    internal class NameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string name = value as string;
            return !name.Any(c => !char.IsLetter(c));
        }
    }
}
