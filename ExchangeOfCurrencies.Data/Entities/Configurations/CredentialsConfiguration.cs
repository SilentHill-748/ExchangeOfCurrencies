using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExchangeOfCurrencies.Data.Entities.Configurations
{
    public class CredentialsConfiguration : IEntityTypeConfiguration<Credentials>
    {
        public void Configure(EntityTypeBuilder<Credentials> builder)
        {
            builder
                .HasKey(x => x.CredentialsId);
            builder
                .Property(x => x.CredentialsId).ValueGeneratedOnAdd();
            builder
                .Property(x => x.Login)
                .HasMaxLength(20)
                .IsRequired();
            builder
                .Property(x => x.Password).IsRequired();
        }
    }
}
