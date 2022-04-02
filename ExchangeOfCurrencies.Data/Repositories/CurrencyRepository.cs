using Microsoft.EntityFrameworkCore;

using ExchangeOfCurrencies.Data.Entities;

namespace ExchangeOfCurrencies.Data.Repositories
{
    public class CurrencyRepository : Repository<Currency>
    {
        public CurrencyRepository(DbContext dbContext) 
            : base(dbContext)
        {

        }
    }
}
