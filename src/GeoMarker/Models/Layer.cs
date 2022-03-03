using System.ComponentModel.DataAnnotations.Schema;
namespace GeoMarker.Models
{
    public class Layer : TenantModelBase
    {
        public Layer(string tenant, string name)
        {
            Name = name;
            Tenant = tenant;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Creator { get; set; }

        [Column(TypeName = "jsonb")]
        public Style Style { get; set; }

        public List<Marker> Markers { get; set; }

        public List<LayerProperty> Properties { get; set; }
    }
}