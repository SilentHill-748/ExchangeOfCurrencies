using Microsoft.EntityFrameworkCore;

using ExchangeOfCurrencies.Data.Entities;
using ExchangeOfCurrencies.Data.Interfaces;

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
