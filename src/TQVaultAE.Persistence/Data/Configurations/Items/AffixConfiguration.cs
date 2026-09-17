using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Persistence.Data.Configurations.Items
{
    // TODO Check only relevant data is mapped. Everything, that can be pulled from game db, should be pulled from there.
    public class AffixConfiguration : IEntityTypeConfiguration<Affix>
    {
        public void Configure(EntityTypeBuilder<Affix> builder)
        {
            builder.ToTable("affix");

            builder.HasKey(x => x.Path);

            builder.Property(x => x.Path)
                   .IsRequired()
                   .HasColumnName("path");
        }
    }
}
