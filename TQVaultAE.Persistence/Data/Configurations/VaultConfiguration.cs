using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TQVaultAE.Model.Vaults;

namespace TQVaultAE.Persistence.Data.Configurations
{
    public class VaultConfiguration : IEntityTypeConfiguration<Vault>
    {
        public void Configure(EntityTypeBuilder<Vault> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type)
                   .IsRequired();

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasMany(x => x.Tabs)
                   .WithOne(x => x.Vault)
                   .HasForeignKey(x => x.VaultId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
