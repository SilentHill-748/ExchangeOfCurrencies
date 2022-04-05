using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore.Query;

namespace ExchangeOfCurrencies.Data.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> Select(
            bool disableTracking = true,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

        TEntity GetById(object pkValue);

        void Update(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);
    }
}
