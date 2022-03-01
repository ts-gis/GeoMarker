using System;
namespace GeoMarker.Models
{
    public class Layer : ModelBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Creator { get; set; }

        public List<Marker> Markers { get; set; }

        public List<LayerProperty> Properties { get; set; }
    }
}