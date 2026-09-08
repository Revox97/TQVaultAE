using System.Drawing;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Persistence.Data.Configurations.Items
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("items");

            //builder.Property(x => x.Id)
            //       .HasColumnName("id");
            //builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(256)
                   .HasColumnName("name");

            //builder.Property(x => x.Classification)
            //       .IsRequired()
            //       .HasColumnName("classification");

            //builder.Property(x => x.Class)
            //       .IsRequired()
            //       .HasColumnName("class");

            builder.Property(x => x.Position)
                   .IsRequired()
                   .HasConversion(
                       pos => JsonSerializer.Serialize(pos),
                       value => JsonSerializer.Deserialize<Point>(value)
                   )
                   .HasColumnName("position")
                   .HasColumnType("TEXT");

            //builder.Property(x => x.Size)
            //       .IsRequired()
            //       .HasConversion(
            //           size => JsonSerializer.Serialize(size),
            //           value => JsonSerializer.Deserialize<Size>(value)
            //       )
            //       .HasColumnName("size")
            //       .HasColumnType("TEXT");

            //builder.Property(x => x.IconDbPath)
            //       .IsRequired()
            //       .HasConversion(
            //           uri => uri.ToString(),
            //           value => new Uri(value)
            //       )
            //       .HasColumnName("icon_db_path")
            //       .HasColumnType("TEXT");

            // TODO might be an issue with lists in SQLLite, maybe a separate table / json parsing is required
            //builder.Property(x => x.BaseItemProperties)
            //       .IsRequired();

            // TODO might be an issue with lists in SQLLite, maybe a separate table / json parsing is required
            //builder.Property(x => x.AdditionalItemProperties)
            //       .IsRequired();

            //builder.Property(x => x.ItemLevel)
            //       .IsRequired()
            //       .HasColumnName("level")
            //       .HasDefaultValue(null);

            //builder.Property(x => x.DatabasePath)
            //       .IsRequired()
            //       .HasColumnName("tq_db_path");
        }
    }
}
