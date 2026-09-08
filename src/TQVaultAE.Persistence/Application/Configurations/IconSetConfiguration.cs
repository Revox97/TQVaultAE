using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TQVaultAE.Model.UI;

namespace TQVaultAE.Persistence.Application.Configurations
{
    public class IconSetConfiguration : IEntityTypeConfiguration<IconSet>
    {
        public void Configure(EntityTypeBuilder<IconSet> builder)
        {
            builder.ToTable("icon_sets");

            builder.Property(x => x.Id)
                   .HasColumnName("id");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IconDown)
                   .HasColumnName("icon_down");

            builder.HasOne(x => x.IconDown)
                   .WithMany()
                   .HasForeignKey(x => x.IconDownId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.IconUp)
                   .HasColumnName("icon_up");

            builder.HasOne(x => x.IconUp)
                   .WithMany()
                   .HasForeignKey(x => x.IconUpId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.IconHover)
                   .HasColumnName("icon_hover");

            builder.HasOne(x => x.IconHover)
                   .WithMany()
                   .HasForeignKey(x => x.IconHoverId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
