using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ExchangeOfCurrencies.ClientModel.Validation;

namespace ExchangeOfCurrencies.ClientModel
{
    // Абстрактный класс для персонала или клиентов.
    public abstract class Person : IValidatableObject
    {
        [Required]
        [Name(ErrorMessage = "Имя указано некорректно!")]
        public string FirstName { get; set; }

        [Required]
        [Name(ErrorMessage = "Фамилия указана некорректно!")]
        public string SecondName { get; set; }

        [Required]
        [Name(ErrorMessage = "Отчество указано некорректно!")]
        public string LastName { get; set; }

        [Required]
        [CustomPhone(ErrorMessage = "Указан неверный формат телефона!")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Указан некорректный e-mail!")]
        public string Email { get; set; }

        [Required]
        [Login(ErrorMessage = "Данный логин уже занят!")]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public Person() { }

        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}
