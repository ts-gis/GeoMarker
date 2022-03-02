using Microsoft.EntityFrameworkCore;
using GeoMarker.Models;
using GeoMarker.Services;

namespace GeoMarker.EFCore;

public class AppDbContext : DbContext
{
    private readonly ITenantService tenantService;

    public DbSet<Layer> Layers { get; set; }

    public DbSet<LayerProperty> LayerProperties { get; set; }

    public DbSet<Marker> Markers { get; set; }

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        ITenantService tenantService)
        : base(options)
    {
        this.tenantService = tenantService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder.Entity<Layer>().HasQueryFilter(x => x.Tenant == tenantService.TenantName);

        base.OnModelCreating(modelBuilder);
    }
}