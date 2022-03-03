using GeoMarker.Models;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace GeoMarker.Dto
{
    public record MarkerRequestDto
    {
        /// <summary>
        /// "geojson" | "wkt" 默认 wkt
        /// </summary>
        [FromQuery(Name = "f")]
        public string GeoFormat { get; set; } = "wkt";

        /// <summary>
        /// 根据标记名搜索
        /// </summary>
        public string Search { get; set; }
    }

    
    public record MarkerDto(int Id, int LayerId, string Name, Geometry Geometry, Style Style);

    public record MarkerCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Geometry Geometry { get; set; }

        public Style Style { get; set; }
    }

    public record MarkerUpdateDto
    {
        public string Name { get; set; }

        public Style Style { get; set; }
    }
}
