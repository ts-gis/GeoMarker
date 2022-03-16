using Microsoft.EntityFrameworkCore;
using GeoMarker.Models;
using GeoMarker.Services;

namespace GeoMarker.Infrastucture.EFCore;

public class AppDbContext : DbContext
{
    private readonly ITenantInfo tenantInfo;

    public DbSet<Layer> Layers { get; set; }

    public DbSet<Marker> Markers { get; set; }

    public AppDbContext(
        DbContextOptions<AppDbContext> options, ITenantInfo tenantInfo)
        : base(options)
    {
        this.tenantInfo = tenantInfo;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder.Entity<Layer>().HasQueryFilter(x => x.Tenant == tenantInfo.Name);
        modelBuilder.Entity<Marker>().HasQueryFilter(x => x.Tenant == tenantInfo.Name);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach(var entry in ChangeTracker.Entries())
        {
            if(entry.State == EntityState.Added && entry.Entity is TenantModelBase model)
            {
                model.Tenant = tenantInfo.Name;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}