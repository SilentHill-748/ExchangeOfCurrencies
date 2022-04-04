using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExchangeOfCurrencies.Data.Entities.Configurations
{
    public class CurrencyRatesConfiguration : IEntityTypeConfiguration<CurrencyRate>
    {
        public void Configure(EntityTypeBuilder<CurrencyRate> builder)
        {
            builder
                .HasKey(x => x.RateId);
            builder
                .Property(x => x.RateId)
                .ValueGeneratedOnAdd();
            builder
                .Property(x => x.Value)
                .HasColumnType("MONEY")
                .IsRequired();
            builder
                .Property(x => x.Date)
                .HasColumnType("DATE")
                .IsRequired();
        }
    }
}
