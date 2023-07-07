using HotelListingApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListingApi.Configurations.Entities
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                new Country { Id = 1, Name = "LA", CountryCode = "51543" },
                new Country { Id = 2, Name = "Cairo", CountryCode = "78453" }
            );
        }
    }
}
