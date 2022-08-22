using Microsoft.EntityFrameworkCore;
using WebAPIProject.Entity.Models;

namespace WebAPIProject.Entity.Data
{
    public class WebAPIContext : DbContext
    {
        public WebAPIContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Boat> Boats { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>();
            modelBuilder.Entity<Bus>();
            modelBuilder.Entity<Boat>();

        }
    }
}
