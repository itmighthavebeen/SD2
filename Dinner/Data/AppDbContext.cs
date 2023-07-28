using Dinner.Models;
using Microsoft.EntityFrameworkCore;

namespace Dinner.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //one to many for dinner menu items

            modelBuilder.Entity<DinnerOrder>()
               .HasMany(dm => dm.MenuItems)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);


        }
        public DbSet<DinnerOrder> DinnerOrders { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
    }
}
