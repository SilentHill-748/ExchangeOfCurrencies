using ExchangeOfCurrencies.Logic;
using ExchangeOfCurrencies.MVVM.Models;
using ExchangeOfCurrencies.Services;

namespace ExchangeOfCurrencies.MVVM.ViewModels
{
    internal class RegistrationViewModel : DialogViewModel
    {
        public RegistrationViewModel()
        {
            ClientModel = new ClientModel();
        }


        public ClientModel ClientModel { get; }


        private void Register(object? parameter)
        {
            User newUser = CreateUser();

            RegistrationService registrationService = new(new ModelMapper());

            registrationService.RegistrationUser(newUser);

            // App.AuthorizedUser = newUser;

            // Close view.
        }

        private User CreateUser()
        {
            string fullName = $"{ClientModel.FirstName} {ClientModel.MiddleName} {ClientModel.LastName}";
            string phone = ClientModel.Phone
                .Replace(" ", "")
                .Replace("+7", "8");

            User newUser = new()
            {
                FullName = fullName,
                Phone = phone,
                Email = ClientModel.Email.ToLower(),
                Login = ClientModel.Login,
                Password = ClientModel.Password,
            };

            return newUser;
        }
    }
}
