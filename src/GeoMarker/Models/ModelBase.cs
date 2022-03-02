using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace GeoMarker.Models
{
    public abstract class ModelBase
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public abstract class TenantModelBase : ModelBase
    {
        [NotNull]
        public string Tenant { get; set; }
    }
}