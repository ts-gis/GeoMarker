using System.Text.Json;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace GeoMarker.Models
{
    public class Marker : ModelBase
    {
        public Marker(int layerId, string name, Geometry geometry)
        {
            LayerId = layerId;
            Name = name;
            Geometry = geometry;
        }

        public int LayerId { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Description { get; set; }

        public Geometry Geometry { get; set; }

        public JsonDocument Properties { get; set; }

        [Column(TypeName = "jsonb")]
        public Style Style { get; set; }
    }
}