using ExchangeOfCurrencies.Data.Entities;
using ExchangeOfCurrencies.Data.Interfaces;
using ExchangeOfCurrencies.Data.Repositories;
using ExchangeOfCurrencies.Logic.Services.Interfaces;

namespace ExchangeOfCurrencies.Logic.Services
{
    public class AutorizationService : IAutorizationService
    {
        private readonly IUnitOfWork _unitOfWork;


        public AutorizationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public bool Autorize(string login, string password)
        {
            ClientRepository clientRepository = (ClientRepository)_unitOfWork.GetRepository<Client>();

            Client? client = clientRepository
                .GetAllLoadedClients(true)
                .FirstOrDefault(x => x.Credentials.Login.Equals(login));

            if (client is null)
                throw new Exception("Client by specified login is not exists!");

            return client.Credentials.Password.Equals(password);
        }
    }
}
