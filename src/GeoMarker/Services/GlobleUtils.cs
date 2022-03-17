using System.Text.Json;

using NetTopologySuite.AspNetCore.Extensions;

namespace GeoMarker.Services
{
    public class GlobleUtils
    {
        public GlobleUtils()
        {
            JsonSerializerOptions = new JsonSerializerOptions();
            JsonSerializerOptions.Converters.AddWktJsonConverter();
        }

        public JsonSerializerOptions JsonSerializerOptions { get; private init; }
    }
}
