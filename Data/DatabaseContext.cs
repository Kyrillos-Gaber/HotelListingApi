using Microsoft.EntityFrameworkCore;

namespace HotelListingApi.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "LA", CountryCode = "51543" },
                new Country { Id = 2, Name = "Cairo", CountryCode = "78453" }
            );

            builder.Entity<Hotel>().HasData(
                new Hotel { Id = 1, Name = "Four Season", CountryId = 2, Rating = 4.5 },
                new Hotel { Id = 2, Name = "Cataract", CountryId = 1, Rating = 4.5 }
            );
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Hotel> Hotels { get; set; }
    }
}
