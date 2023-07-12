using Dinner.Models;
using Microsoft.EntityFrameworkCore;

namespace Dinner.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        ///<Summary>
        /// Get these AppDbContext commetns
        ///</Summary>
        public DbSet<DinnerOrder> DinnerOrders { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
    }
}
