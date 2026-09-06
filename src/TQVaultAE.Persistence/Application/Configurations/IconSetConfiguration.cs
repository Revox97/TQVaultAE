using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TQVaultAE.Model;

namespace TQVaultAE.Persistence.Application.Configurations
{
    public class IconSetConfiguration : IEntityTypeConfiguration<IconSet>
    {
        public void Configure(EntityTypeBuilder<IconSet> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.IconDown)
                   .WithMany()
                   .HasForeignKey(x => x.IconDownId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.IconUp)
                   .WithMany()
                   .HasForeignKey(x => x.IconUpId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.IconHover)
                   .WithMany()
                   .HasForeignKey(x => x.IconHoverId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
