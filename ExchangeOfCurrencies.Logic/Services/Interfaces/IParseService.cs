namespace ExchangeOfCurrencies.Logic.Services.Interfaces
{
    public interface IParseService
    {
        Task<object> ParseAsync(string url);
    }
}
