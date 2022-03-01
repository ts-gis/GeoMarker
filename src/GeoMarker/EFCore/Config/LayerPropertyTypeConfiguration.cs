using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GeoMarker.Models;

namespace GeoMarker.EFCore.Config
{
    public class LayerPropertyTypeConfiguration : IEntityTypeConfiguration<LayerProperty>
    {
        public void Configure(EntityTypeBuilder<LayerProperty> LayerProperty)
        {
            LayerProperty.Property(lp=>lp.Type).HasConversion<string>();
        }
    }
}