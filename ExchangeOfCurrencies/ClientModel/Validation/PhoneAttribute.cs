using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ExchangeOfCurrencies.ClientModel.Validation
{
    internal class CustomPhoneAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string phone = value as string;
            return Regex.IsMatch(phone, @"\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}"); // +7 (123) 456-78-90
        }
    }
}
