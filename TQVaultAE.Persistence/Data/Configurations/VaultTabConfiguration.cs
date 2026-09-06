using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TQVaultAE.Model.Vaults;

namespace TQVaultAE.Persistence.Data.Configurations
{
    public class VaultTabConfiguration : IEntityTypeConfiguration<VaultTab>
    {
        public void Configure(EntityTypeBuilder<VaultTab> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasOne(x => x.Icon)
                   .WithMany()
                   .HasForeignKey(x => x.IconId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Items)
                   .WithOne()
                   .HasForeignKey(x => x.Id)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
