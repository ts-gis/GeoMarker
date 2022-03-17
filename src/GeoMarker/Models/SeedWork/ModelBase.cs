using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using GeoMarker.Infrastucture.Attributes;

namespace GeoMarker.Models.SeedWork
{
    public abstract class ModelBase
    {
        [GeojsonTag]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}