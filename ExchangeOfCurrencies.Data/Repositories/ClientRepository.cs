using Microsoft.EntityFrameworkCore;

using ExchangeOfCurrencies.Data.Entities;

namespace ExchangeOfCurrencies.Data.Repositories
{
    public class ClientRepository : Repository<Client>
    {
        public ClientRepository(DbContext dbContext) 
            : base(dbContext)
        {

        } 
    }
}
