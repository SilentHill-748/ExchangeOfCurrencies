using System.Linq.Expressions;

using ExchangeOfCurrencies.Data.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;

namespace ExchangeOfCurrencies.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> _set;


        public Repository(DbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext);

            _set = dbContext.Set<TEntity>();
        }


        public void Add(TEntity entity)
        {
            _set.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _set.AddRange(entities);
        }

        public void Delete(TEntity entity)
        {
            _set.Remove(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            _set.RemoveRange(entities);
        }

        public TEntity GetById(object pkValue)
        {
            TEntity? entity = _set.Find(pkValue);

            if (entity is null)
                throw new ArgumentException($"{typeof(TEntity).Name} by specified primary key isn't found!");

            return entity;
        }

        public IEnumerable<TEntity> Select(
            bool disableTracking = true,
            Expression<Func<TEntity, bool>>? predicate = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = _set;

            if (disableTracking)
                query = _set.AsNoTracking();

            if (predicate is not null)
                query = query.Where(predicate);

            if (include is not null)
                query = include(query);

            return query;
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            _set.UpdateRange(entities);
        }

        public void Update(TEntity entity)
        {
            _set.Update(entity);
        }
    }
}
