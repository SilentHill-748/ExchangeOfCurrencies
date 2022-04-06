using Microsoft.EntityFrameworkCore;

using ExchangeOfCurrencies.Data.Interfaces;
using System.Transactions;

namespace ExchangeOfCurrencies.Data
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : DbContext
    {
        private Dictionary<Type, object>? _repositories;
        private bool disposedValue;


        public UnitOfWork(TContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        public TContext DbContext { get; }


        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories is null)
                _repositories = new Dictionary<Type, object>();

            Type entityType = typeof(TEntity);
            if (!_repositories.ContainsKey(entityType))
            {
                _repositories[entityType] = GetRepositoryByEntity<TEntity>();
            }

            return (IRepository<TEntity>)_repositories[entityType];
        }

        public int Save()
        {
            return DbContext.SaveChanges();
        }

        public async Task<int> SaveAsync(params IUnitOfWork[] unitOfWorks)
        {
            int count = 0;

            using TransactionScope ts = new(TransactionScopeAsyncFlowOption.Enabled);

            for (int i = 0; i < unitOfWorks.Length; i++)
                count += await unitOfWorks[i].SaveAsync();

            count += await DbContext.SaveChangesAsync();

            ts.Complete();

            return count;
        }

        public async Task<int> SaveAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                    _repositories?.Clear();
                }

                disposedValue = true;
            }
        }

        private object GetRepositoryByEntity<TEntity>()
            where TEntity : class
        {
            Type baseRepositoryType = typeof(Repositories.Repository<TEntity>);

            Type repositoryType = typeof(TEntity).Assembly
                .GetTypes()
                .First(x => 
                    x.BaseType is not null && 
                    x.BaseType.Equals(baseRepositoryType));

            return Activator.CreateInstance(repositoryType, new object[] { DbContext })!;
        }
    }
}
