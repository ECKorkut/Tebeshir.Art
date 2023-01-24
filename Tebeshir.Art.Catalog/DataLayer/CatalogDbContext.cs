using Microsoft.EntityFrameworkCore;
using Tebeshir.Art.Catalog.DataLayer.Model;

namespace Tebeshir.Art.Catalog.DataLayer
{
    public class CatalogDbContext:DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CatalogModel> CatalogModels { get; set; }
    }
}
