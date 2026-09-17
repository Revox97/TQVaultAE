using System.Drawing;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Persistence.Data.Configurations.Items
{
    // TODO Check only relevant data is mapped. Everything, that can be pulled from game db, should be pulled from there.
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("items");

            builder.Property(x => x.Position)
                   .HasConversion(
                        pos => $"{pos.X},{pos.Y}",
                        value => new Point(int.Parse(value.Split(',')[0]), int.Parse(value.Split(',')[1]))
                   )
                   .HasColumnName("position")
                   .HasColumnType("TEXT");

            builder.Property(x => x.ResourcePath)
                   .IsRequired()
                   .HasColumnName("resource_path");

            builder.Property(x => x.Seed)
                   .IsRequired()
                   .HasColumnName("seed");

            builder.HasOne(x => x.Prefix)
                   .WithMany()
                   .HasForeignKey(x => x.PrefixId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.PrefixId)
                   .HasColumnName("prefix");

            builder.HasOne(x => x.Suffix)
                   .WithMany()
                   .HasForeignKey(x => x.SuffixId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.SuffixId)
                   .HasColumnName("suffix");

            builder.HasOne(x => x.TalismanOne)
                   .WithMany()
                   .HasForeignKey(x => x.TalismanOneId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.TalismanOneId)
                   .HasColumnName("talisman_one");

            builder.HasOne(x => x.TalismanTwo)
                   .WithMany()
                   .HasForeignKey(x => x.TalismanTwoId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.TalismanTwoId)
                   .HasColumnName("talisman_two");

            builder.Property(x => x.Var1)
                   .HasColumnName("var1");

            builder.Property(x => x.Var2)
                   .HasColumnName("var2");

            builder.Property(x => x.Classification)
                   .IsRequired()
                   .HasColumnName("classification");

            builder.Property(x => x.Cost)
                   .HasColumnName("cost");

            builder.Property(x => x.Level)
                   .HasColumnName("level");

            builder.Property(x => x.MaxTransparency)
                   .HasColumnName("max_transparency");

            builder.Property(x => x.Scale)
                   .HasColumnName("scale");

            builder.Property(x => x.TemplateName)
                   .HasColumnName("template_name");

            builder.Property(x => x.Size)
                   .IsRequired()
                   .HasConversion(
                        size => $"{size.Height},{size.Width}",
                        value => new Size(int.Parse(value.Split(',')[0]), int.Parse(value.Split(',')[1]))
                   )
                   .HasColumnName("size")
                   .HasColumnType("TEXT");

            builder.Property(x => x.Class)
                   .IsRequired()
                   .HasColumnName("class");

            builder.Property(x => x.StackCount)
                   .IsRequired()
                   .HasColumnName("stack_count");

            builder.Property(x => x.CanStack)
                   .IsRequired()
                   .HasColumnName("can_stack");
        }
    }
}
