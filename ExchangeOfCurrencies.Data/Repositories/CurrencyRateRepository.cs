using Microsoft.EntityFrameworkCore;

using ExchangeOfCurrencies.Data.Entities;

namespace ExchangeOfCurrencies.Data.Repositories
{
    public class CurrencyRateRepository : Repository<CurrencyRate>
    {
        public CurrencyRateRepository(DbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
