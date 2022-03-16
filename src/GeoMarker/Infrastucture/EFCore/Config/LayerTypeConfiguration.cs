using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GeoMarker.Models;

namespace GeoMarker.Infrastucture.EFCore.Config
{
    public class LayerTypeConfiguration : IEntityTypeConfiguration<Layer>
    {
        public void Configure(EntityTypeBuilder<Layer> layer)
        {
            layer.HasMany(l => l.Markers)
                 .WithOne(m => m.Layer)
                 .HasForeignKey(m => m.LayerId)
                 .OnDelete(DeleteBehavior.Cascade);

            layer.HasIndex(l => new { l.Tenant, l.Name }).IsUnique();
        }
    }
}