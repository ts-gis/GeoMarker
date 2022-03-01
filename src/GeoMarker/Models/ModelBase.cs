using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoMarker.Models
{
    public abstract class ModelBase
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Tenant { get; set; }

        public DateTime CreateTime { get; set; }
    }
}