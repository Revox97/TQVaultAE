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

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .IsRequired()
                   .HasColumnName("id");

            builder.Property(x => x.Name)
                   .IsRequired();

            builder.HasOne(x => x.Settings)
                   .WithMany()
                   .HasForeignKey(x => x.SettingsId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }
    }
}
