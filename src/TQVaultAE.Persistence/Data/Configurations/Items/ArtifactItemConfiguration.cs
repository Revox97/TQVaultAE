using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TQVaultAE.Model.Enumerations;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Persistence.Data.Configurations.Items
{
    public class ArtifactItemConfiguration : IEntityTypeConfiguration<ArtifactItem>
    {
        public void Configure(EntityTypeBuilder<ArtifactItem> builder)
        {
            builder.Property(x => x.Class)
                   .HasDefaultValue(ItemClass.ItemArtifact);
        }
    }
}
