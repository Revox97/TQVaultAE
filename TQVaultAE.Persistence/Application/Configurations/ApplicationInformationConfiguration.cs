using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TQVaultAE.Model;

namespace TQVaultAE.Persistence.Application.Configurations
{
    public class ApplicationInformationConfiguration : IEntityTypeConfiguration<ApplicationInformation>
    {
        public void Configure(EntityTypeBuilder<ApplicationInformation> builder)
        {
            builder.ToTable("application");

            builder.HasKey(x => x.Name);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("name");

            builder.Property(x => x.Version)
                .IsRequired()
                .HasColumnName("version");
        }
    }
}
