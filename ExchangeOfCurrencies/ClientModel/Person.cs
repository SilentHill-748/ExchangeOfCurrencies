using System.ComponentModel.DataAnnotations;
using ExchangeOfCurrencies.ClientModel.Validation;

namespace ExchangeOfCurrencies.ClientModel
{
    // Абстрактный класс для персонала или клиентов.
    public abstract class Person
    {
        public int UserId { get; set; }

        [Required(ErrorMessage ="Поле ввода имени обязательно!")]
        [Name(ErrorMessage = "Имя указано некорректно!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле ввода фамилии обязательно!")]
        [Name(ErrorMessage = "Фамилия указана некорректно!")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Поле ввода отчества обязательно!")]
        [Name(ErrorMessage = "Отчество указано некорректно!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не указан номер телефона!")]
        [CustomPhone(ErrorMessage = "Указан неверный формат телефона!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не указан Email-адрес!")]
        [Email(ErrorMessage = "Указан некорректный e-mail!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Необходимо указать логин!")]
        [Login]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль!")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage ="Пароли не совпали!")]
        public string ConfirmPassword { get; set; }

        public Person() { }
    }
}
