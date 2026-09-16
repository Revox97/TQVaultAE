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

            //builder.Property(x => x.IconDownResourcePath)
            //       .IsRequired()
            //       .HasColumnName("icon_down_resource_path");

            //builder.Property(x => x.IconUpResourcePath)
            //       .IsRequired()
            //       .HasColumnName("icon_up_resource_path");

            //builder.Property(x => x.IconHoverResourcePath)
            //       .IsRequired()
            //       .HasColumnName("icon_hover_resource_path");
        }
    }
}
