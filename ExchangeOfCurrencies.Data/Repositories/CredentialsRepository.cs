using Microsoft.EntityFrameworkCore;

using ExchangeOfCurrencies.Data.Entities;

namespace ExchangeOfCurrencies.Data.Repositories
{
    public class CredentialsRepository : Repository<Credentials>
    {
        public CredentialsRepository(DbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
