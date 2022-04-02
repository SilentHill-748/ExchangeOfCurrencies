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
            Clients = Set<Client>();
            Credentials = Set<Credentials>();
            Wallets = Set<Wallet>();
            Currencies = Set<Currency>();
            CurrencyRates = Set<CurrencyRate>();
        }


        public DbSet<Client> Clients { get; }
        public DbSet<Wallet> Wallets { get; }
        public DbSet<Credentials> Credentials { get; }
        public DbSet<Currency> Currencies { get; }
        public DbSet<CurrencyRate> CurrencyRates { get; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
