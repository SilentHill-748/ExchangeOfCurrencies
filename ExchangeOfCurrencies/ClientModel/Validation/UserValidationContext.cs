using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExchangeOfCurrencies.ClientModel.Validation
{
    /// <summary>
    ///     Контекст валидации входных данных пользователя
    /// </summary>
    public class UserValidationContext
    {
        private readonly User validateUser;

        public List<ValidationResult> Errors { get; }

        public UserValidationContext(List<string> personalData)
        {
            Errors = new List<ValidationResult>();
            validateUser = new User(personalData);
        }

        public bool Validate()
        {
            try
            {
                ValidationContext context = new (validateUser);
                return Validator.TryValidateObject(validateUser, context,
                    Errors, true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
