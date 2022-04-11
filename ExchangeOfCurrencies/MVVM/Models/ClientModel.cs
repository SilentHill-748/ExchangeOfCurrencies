using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

using ExchangeOfCurrencies.Core.Validation;

namespace ExchangeOfCurrencies.MVVM.Models
{
    internal class ClientModel : IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                return columnName switch
                {
                    "FirstName" => ValidateValue(FirstName, ValidationPatterns.NamePattern, ErrorMessages.NameValidationError),
                    "MiddleName" => ValidateValue(MiddleName, ValidationPatterns.NamePattern, ErrorMessages.NameValidationError),
                    "LastName" => ValidateValue(LastName, ValidationPatterns.NamePattern, ErrorMessages.NameValidationError),
                    "Email" => ValidateValue(Email, ValidationPatterns.EmailPattern, ErrorMessages.EmailValidationError),
                    "Phone" => ValidateValue(Phone, ValidationPatterns.PhonePattern, ErrorMessages.PhoneValidationError),
                    "Login" => ValidateValue(Login, ValidationPatterns.LoginPattern, ErrorMessages.LoginValidationError),
                    "ConfirmPassword" => ValidatePassword(Password, ConfirmPassword),
                    _ => string.Empty
                };
            }
        }


        public int ClientId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string Error => throw new NotImplementedException();


        private static string ValidateValue(string value, string pattern, string message)
        {
            if (!string.IsNullOrEmpty(value) && !Regex.IsMatch(value, pattern))
            {
                return message;
            }

            return string.Empty;
        }

        private static string ValidatePassword(string password, string confirmPassword)
        {
            if (!password.Equals(confirmPassword))
                return "Пароли не совпадают!";

            return string.Empty;
        }
    }
}
