using Dinner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection.Emit;

namespace Dinner.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        ///<Summary>
        /// Get these AppDbContext commetns
        ///</Summary>
        ///

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
