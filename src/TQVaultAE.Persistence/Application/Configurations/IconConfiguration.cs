using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TQVaultAE.Model;

namespace TQVaultAE.Persistence.Application.Configurations
{
    public class IconConfiguration : IEntityTypeConfiguration<Icon>
    {
        public void Configure(EntityTypeBuilder<Icon> builder)
        {
            builder.ToTable("icons");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Uri)
                   .HasConversion(
                       uri => uri.ToString(),
                       value => new Uri(value)
                   )
                   .HasColumnType("TEXT");
        }
    }
}
