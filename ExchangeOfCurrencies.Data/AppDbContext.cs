using System.Reflection;

using Microsoft.EntityFrameworkCore;

using ExchangeOfCurrencies.Data.Entities;

namespace ExchangeOfCurrencies.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }


        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Wallet> Wallets => Set<Wallet>();
        public DbSet<Credentials> Credentials => Set<Credentials>();
        public DbSet<Currency> Currencies => Set<Currency>();
        public DbSet<CurrencyRate> CurrencyRates => Set<CurrencyRate>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
