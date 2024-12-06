using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infrastructrue.Efcore.Mapping;

namespace ShopManagement.Infrastructrue.Efcore
{
    public class ShopContext : DbContext
    {
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ProductCategoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
