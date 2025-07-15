using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenTable.Models.DataLayer.Configuration;

namespace OpenTable.Models.DataLayer
{
    public class OpenTableDbContext : IdentityDbContext<User>
    {
        public OpenTableDbContext(DbContextOptions<OpenTableDbContext> options)
          : base(options)
        { }

        public DbSet<Metropolis> Metropolis { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Restaurant> Restaurant { get; set; } = null!;
        public DbSet<Cuisines> Cuisines { get; set; } = null!;
        public DbSet<PriceRange> PriceRange { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ConfigureCuisines());
            modelBuilder.ApplyConfiguration(new ConfigureMetropolis());
            modelBuilder.ApplyConfiguration(new ConfigurePriceRange());
            modelBuilder.ApplyConfiguration(new ConfigureRestaurant());
        }
    }
}
