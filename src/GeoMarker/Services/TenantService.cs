namespace GeoMarker.Services;

public class TenantInfo
{
    public int MaxLayerCount { get; set; } 
}

public interface ITenantService
{
    string TenantName { get; set; }

    TenantInfo Info { get; set; }
}

public class TenantService : ITenantService
{
    public string TenantName { get; set; }

    public TenantInfo Info { get; set; } = new TenantInfo();
}
