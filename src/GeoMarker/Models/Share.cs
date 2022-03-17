using GeoMarker.Models.SeedWork;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace GeoMarker.Models
{
    public class Share : TenantModelBase
    {
        public Share(string name, string value)
        {

        }

        [NotNull]
        public string Name { get; set; }

        public string Value { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Expire { get; set; }
    }
}
