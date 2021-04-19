﻿using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace ExchangeOfCurrencies.ClientModel.Validation
{
    internal class CustomPhoneAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string phone = value as string;
            return Regex.IsMatch(phone, @"\+7 \d{3} \d{3}\d{2}\d{2}"); // +7 123 4567890
        }
    }
}
