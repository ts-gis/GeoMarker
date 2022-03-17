using System.Diagnostics.CodeAnalysis;

namespace GeoMarker.Models.SeedWork
{
    public abstract class TenantModelBase : ModelBase
    {
        public TenantModelBase()
        {
        }

        [NotNull]
        public string Tenant { get; set; }
    }
}
