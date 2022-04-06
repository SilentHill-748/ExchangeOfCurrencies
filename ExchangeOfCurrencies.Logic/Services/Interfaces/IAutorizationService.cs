namespace ExchangeOfCurrencies.Logic.Services.Interfaces
{
    public interface IAutorizationService
    {
        bool Autorize(string login, string password);
    }
}
