using System.ComponentModel.DataAnnotations.Schema;
namespace GeoMarker.Models
{
    public class Layer : TenantModelBase
    {
        public Layer(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public string? Description { get; set; }

        [Column(TypeName = "jsonb")]
        public Style Style { get; set; } = new Style();

        public List<Marker>? Markers { get; set; }
    }
}