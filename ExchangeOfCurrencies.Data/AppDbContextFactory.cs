using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace ExchangeOfCurrencies.Data
{
    public class AppDbContextFactory : IDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext()
        {
            string connectionString = "Server=localhost;Database=ExchangeOfCurrency;User=silent748;Password=H4o1w[3!c%sD6Z'!4Jv>C";

            DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
