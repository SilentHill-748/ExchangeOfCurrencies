using Microsoft.EntityFrameworkCore;

namespace ExchangeOfCurrencies.Data.Interfaces
{
    public interface IUnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        TContext DbContext { get; }

        Task<int> SaveAsync(params IUnitOfWork[] unitOfWorks);
    }
}
