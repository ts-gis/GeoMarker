namespace GeoMarker.Infrastucture.Configuration
{
    public class LimitConfig
    {
        public const string SectionName = "Limit";

        public int MaxLayer { get; set; }

        public int MaxMarker { get; set; }

        public int MaxShare { get; set; }

    }
}
