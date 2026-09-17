using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TQVaultAE.Model.UI;

namespace TQVaultAE.Persistence.Data.Configurations
{
    public class IconSetConfiguration : IEntityTypeConfiguration<IconSet>
    {
        public void Configure(EntityTypeBuilder<IconSet> builder)
        {
            builder.ToTable("icon_sets");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .IsRequired()
                   .HasColumnName("id");

            builder.HasOne(x => x.IconDown)
                   .WithMany()
                   .HasForeignKey(x => x.IconDownId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.IconDownId)
                   .IsRequired()
                   .HasColumnName("icon_down_id");

            builder.HasOne(x => x.IconUp)
                   .WithMany()
                   .HasForeignKey(x => x.IconUpId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.IconUpId)
                   .IsRequired()
                   .HasColumnName("icon_up_id");

            builder.HasOne(x => x.IconHover)
                   .WithMany()
                   .HasForeignKey(x => x.IconHoverId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.IconHoverId)
                   .IsRequired()
                   .HasColumnName("icon_hover_id");
        }
    }
}
