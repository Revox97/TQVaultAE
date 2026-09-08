using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TQVaultAE.Model;

namespace TQVaultAE.Persistence.Application.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.Property(x => x.Id)
                   .IsRequired()
                   .HasColumnName("id");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasColumnName("name");

            builder.Property(x => x.Settings)
                   .HasColumnName("settings_id");

            builder.HasOne(x => x.Settings)
                   .WithMany()
                   .HasForeignKey(x => x.SettingsId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }
    }
}
