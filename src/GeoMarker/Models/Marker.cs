using System.Text.Json;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoMarker.Models
{
    public class Marker : ModelBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Geometry Geometry { get; set; }
        
        public JsonDocument Properties { get; set; }

        [Column(TypeName = "jsonb")]
        public Style Style { get; set; }
    }
}