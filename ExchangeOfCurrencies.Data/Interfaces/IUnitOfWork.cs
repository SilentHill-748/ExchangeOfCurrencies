namespace ExchangeOfCurrencies.Data.Interfaces
{
    public interface IUnitOfWork : IRepositoryFactory, IDisposable
    {
        int Save();

        Task<int> SaveAsync();
    }
}
