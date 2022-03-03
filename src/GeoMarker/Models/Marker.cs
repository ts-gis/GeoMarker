using System.Text.Json;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using GeoMarker.Attributes;

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

        [GeojsonTag]
        public int LayerId { get; set; }

        public Layer Layer { get; set; }

        [GeojsonTag]
        [NotNull]
        public string Name { get; set; }

        public string Description { get; set; }

        public Geometry Geometry { get; set; }

        public JsonDocument Properties { get; set; }

        [GeojsonTag]
        [Column(TypeName = "jsonb")]
        public Style Style { get; set; }
    }
}