using ExchangeOfCurrencies.MVVM.Models;

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

            // mapping and send to Db

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
