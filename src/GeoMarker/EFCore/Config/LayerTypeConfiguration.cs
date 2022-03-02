using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GeoMarker.Models;

namespace GeoMarker.EFCore.Config
{
    public class LayerTypeConfiguration : IEntityTypeConfiguration<Layer>
    {
        public void Configure(EntityTypeBuilder<Layer> layer)
        {
            layer.HasMany(l => l.Markers)
                 .WithOne()
                 .HasForeignKey(m => m.LayerId)
                 .OnDelete(DeleteBehavior.Cascade);

            layer.HasMany(l => l.Properties)
                 .WithOne()
                 .HasForeignKey(lp => lp.LayerId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}