using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExchangeOfCurrencies.Data.Entities.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder
                .HasKey(x => x.ClientId);
            builder
                .Property(x => x.ClientId)
                .ValueGeneratedOnAdd();
            builder
                .HasIndex(x => x.Email)
                .IsUnique(true);
            builder
                .HasIndex(x => x.Phone)
                .IsUnique(true);
            builder
                .Property(x => x.FullName)
                .IsRequired();
        }
    }
}
