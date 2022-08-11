using Microsoft.EntityFrameworkCore;

namespace EFDemo
{
    public class BloggingContext : DbContext
    {
        public DbSet<Good> Goods { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<GoodOnWarehouse> GoodsOnWarehouses { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GoodOnWarehouse>().HasKey(x => new { x.GoodId, x.WarehouseId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=EPUADNIW02B7;Initial Catalog=AdvancedNew;Integrated Security=True");
        }
    }
}
