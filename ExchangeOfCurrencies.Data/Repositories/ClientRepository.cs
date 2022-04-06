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

        /// <summary>
        /// Get all clients from DB with all included properties. This is extension for Select function.
        /// </summary>
        /// <param name="asNotTracking">
        ///     <see langword="true"/> to disable changing tracking; otherwise, <see langword="false"/>. 
        ///     Default to <see langword="true"/></param>
        /// <returns>A <see cref="List{T}"/> of all clients from DB</returns>
        public List<Client> GetAllLoadedClients(bool asNotTracking = true)
        {
            return Select(
                asNotTracking,
                include: x => x
                    .Include(x => x.Credentials)
                    .Include(x => x.Wallet))
                .ToList();
        }
    }
}
