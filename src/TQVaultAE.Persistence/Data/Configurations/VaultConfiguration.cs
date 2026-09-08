using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TQVaultAE.Model.Vaults;

namespace TQVaultAE.Persistence.Data.Configurations
{
    public class VaultConfiguration : IEntityTypeConfiguration<Vault>
    {
        public void Configure(EntityTypeBuilder<Vault> builder)
        {
            builder.ToTable("vaults");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("id");

            builder.Property(x => x.Type)
                   .IsRequired()
                   .HasColumnName("type");

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(200)
                   .HasColumnName("name");

            builder.Property(x => x.Tabs)
                   .HasColumnName("tabs");

            builder.HasMany(x => x.Tabs)
                   .WithOne(x => x.Vault)
                   .HasForeignKey(x => x.VaultId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
