using GeoMarker.Models;
using System.ComponentModel.DataAnnotations;

namespace GeoMarker.Dto
{
    public record LayerDto(int Id, string Name)
    {
        public string Description { get; init; }
    }

    public record LayerDetailDto(int Id, string Name) : LayerDto(Id, Name)
    {
        public List<Marker> Markers { get; init; }

        public Style Style { get; init; }
    }

    public record LayerCreateDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public record LayerUpdateDto
    {
        public string Description { get; set; }

        public Style Style { get; set; }
    }
}
