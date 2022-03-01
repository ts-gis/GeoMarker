namespace GeoMarker.Models
{
    public class LayerProperty : ModelBase
    {
        public int LayerId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public PropertyType Type { get; set; }

        public bool Display { get; set; } = true;

        public int Order { get; set; }
    }
}