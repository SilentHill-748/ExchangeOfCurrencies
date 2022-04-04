﻿namespace ExchangeOfCurrencies.Data.Interfaces
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> GetRepository<TEntity>() 
            where TEntity: class;
    }
}
