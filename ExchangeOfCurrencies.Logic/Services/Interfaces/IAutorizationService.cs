namespace ExchangeOfCurrencies.Logic.Services.Interfaces
{
    public interface IAutorizationService
    {
        Task<bool> AutorizeAsync(string login, string password);
    }
}
