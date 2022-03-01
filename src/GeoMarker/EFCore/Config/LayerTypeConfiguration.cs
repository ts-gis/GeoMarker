using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GeoMarker.Models;

namespace GeoMarker.EFCore.Config
{
    public class LayerTypeConfiguration : IEntityTypeConfiguration<Layer>
    {
        public void Configure(EntityTypeBuilder<Layer> layer)
        {
            layer.HasMany(l => l.Markers).WithOne().OnDelete(DeleteBehavior.Cascade);
            layer.HasMany(l => l.Properties).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}