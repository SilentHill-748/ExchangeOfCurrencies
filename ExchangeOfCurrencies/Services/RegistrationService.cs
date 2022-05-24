using ExchangeOfCurrencies.Data.Entities;
using ExchangeOfCurrencies.MVVM.Models;
using ExchangeOfCurrencies.Logic.Interfaces;

namespace ExchangeOfCurrencies.Services
{
    internal class RegistrationService
    {
        private readonly IModelMapper _modelMapper;


        public RegistrationService(IModelMapper modelMapper)
        {
            _modelMapper = modelMapper;
        }


        public void RegistrationUser(User user)
        {
            Client client = _modelMapper.Map<Client, User>(user);
            Credentials credentials = _modelMapper.Map<Credentials, User>(user);

            client.Credentials = credentials;

            App.UnitOfWork.GetRepository<Client>().Add(client);
            App.UnitOfWork.Save();
        }
    }
}
