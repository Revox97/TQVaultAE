using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TQVaultAE.Model.Vaults;
using TQVaultAE.Persistence.Data.Entities;

namespace TQVaultAE.Persistence.Data.Configurations
{
    public class VaultTabConfiguration : IEntityTypeConfiguration<VaultTab>
    {
        public void Configure(EntityTypeBuilder<VaultTab> builder)
        {
            builder.ToTable("vault_tabs");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("id");

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(200)
                   .HasColumnName("name");

            builder.HasOne(x => x.Vault)
                   .WithMany()
                   .HasForeignKey(x => x.VaultId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.VaultId)
                   .IsRequired()
                   .HasColumnName("vault_id");

            builder.HasOne(x => x.IconSet)
                   .WithMany()
                   .HasForeignKey(x => x.IconSetId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.IconSetId)
                   .IsRequired()
                   .HasColumnName("icon_id");

            builder.HasMany(x => x.Items)
                   .WithMany()
                   .UsingEntity<ItemToVaultTab>(
                        j => j.HasOne(x => x.Item)
                              .WithMany()
                              .HasForeignKey(x => x.ItemId),

                        j => j.HasOne(x => x.VaultTab)
                              .WithMany()
                              .HasForeignKey(x => x.VaultTabId),

                        j =>
                        {
                            j.ToTable("vault_tab_item");

                            j.HasKey(x => new
                            {
                                x.VaultTabId,
                                x.ItemId
                            });

                            j.Property(x => x.VaultTabId)
                             .IsRequired()
                             .HasColumnName("vault_tab_id");

                            j.Property(x => x.ItemId)
                             .IsRequired()
                             .HasColumnName("item_id");
                        }
                   );
        }
    }
}
