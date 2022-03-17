using System.Reflection;

using NetTopologySuite.AspNetCore.Extensions;
using NetTopologySuite.Features;

using GeoMarker.Infrastucture.Attributes;
using GeoMarker.Models;

namespace GeoMarker.Infrastucture.Extensions
{
    public static class GeoModelExtension
    {
        public static async Task<string> ConvertToFeatureCollectionAsync(this IEnumerable<Marker> markers)
        {
            var featureCollection = new FeatureCollection();
            foreach (var mark in markers)
            {
                var attributes = new AttributesTable();
                foreach (var prop in mark.GetType().GetProperties())
                {
                    var tag = prop.GetCustomAttribute<GeojsonTagAttribute>();
                    if (tag != null)
                    {
                        var name = tag.Name ?? prop.Name;
                        name = name.First().ToString().ToLower() + name.Substring(1);
                        attributes.Add(name, prop.GetValue(mark));
                    }
                }

                featureCollection.Add(new Feature
                {
                    Geometry = mark.Geometry,
                    Attributes = attributes
                });
            }

            return await SpatialDataConverter.FeaturesToGeoJsonAsync(featureCollection);
        } 
    }
}
