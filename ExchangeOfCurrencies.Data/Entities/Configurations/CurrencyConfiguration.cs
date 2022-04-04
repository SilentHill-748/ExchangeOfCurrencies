using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExchangeOfCurrencies.Data.Entities.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder
                .HasKey(x => x.CurrencyId);
            builder
                .Property(x => x.CurrencyId)
                .ValueGeneratedOnAdd();
            builder
                .Property(x => x.Name)
                .IsRequired();
            builder
                .Property(x => x.EngName)
                .IsRequired();
        }
    }
}
