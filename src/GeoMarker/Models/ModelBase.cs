using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

using GeoMarker.Infrastucture.Attributes;

namespace GeoMarker.Models
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

    public abstract class TenantModelBase : ModelBase
    {
        public TenantModelBase()
        {
        }

        [NotNull]
        public string Tenant { get; set; }
    }
}