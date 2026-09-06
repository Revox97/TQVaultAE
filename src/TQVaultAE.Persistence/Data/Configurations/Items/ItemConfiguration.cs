using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Persistence.Data.Configurations.Items
{
    public class ItemConfiguration : IEntityTypeConfiguration<ItemBase>
    {
        public void Configure(EntityTypeBuilder<ItemBase> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.Classification)
                .IsRequired();

            builder.Property(x => x.Class)
                .IsRequired();

            builder.Property(x => x.Position)
                .IsRequired();

            builder.Property(x => x.Size)
                .IsRequired();

            // Icon (linker table)
            builder.Property(x => x.IconDbPath)
                .IsRequired()
                .HasConversion(
                    uri => uri.ToString(),
                    value => new Uri(value)
                )
                .HasColumnType("TEXT");

            // TODO might be an issue with lists in SQLLite, maybe a separate table / json parsing is required
            builder.Property(x => x.BaseItemProperties)
                .IsRequired();

            // TODO might be an issue with lists in SQLLite, maybe a separate table / json parsing is required
            builder.Property(x => x.AdditionalItemProperties)
                .IsRequired();

            builder.Property(x => x.ItemLevel)
                .IsRequired()
                .HasDefaultValue(null);

            builder.Property(x => x.DatabasePath)
                .IsRequired();
        }
    }
}
