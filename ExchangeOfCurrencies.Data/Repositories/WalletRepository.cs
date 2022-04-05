using Microsoft.EntityFrameworkCore;

using ExchangeOfCurrencies.Data.Entities;

namespace ExchangeOfCurrencies.Data.Repositories
{
    public class WalletRepository : Repository<Wallet>
    {
        public WalletRepository(DbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
