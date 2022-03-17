using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;

using GeoMarker.Models;

namespace GeoMarker.Controllers.Dto
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
        [Required(ErrorMessage = "标记名不能为空")]
        public string Name { get; set; }

        [Required(ErrorMessage = "标记地理数据不能为空")]
        public Geometry Geometry { get; set; }

        public Style Style { get; set; }
    }

    public record MarkerUpdateDto
    {
        [Required(ErrorMessage = "标记名不能为空")]
        public string Name { get; set; }

        public Style Style { get; set; }
    }
}
