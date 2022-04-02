namespace ExchangeOfCurrencies.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();

        Task<int> SaveAsync();
    }
}
