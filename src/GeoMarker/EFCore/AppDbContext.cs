using Microsoft.EntityFrameworkCore;
using GeoMarker.Models;

namespace GeoMarker.EFCore
{
    public class AppDbContext : DbContext
    {
        public DbSet<Layer> Layers { get; set; }

        public DbSet<LayerProperty> LayerProperties { get; set; }

        public DbSet<Marker> Markers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
