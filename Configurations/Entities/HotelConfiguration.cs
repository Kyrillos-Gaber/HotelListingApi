using HotelListingApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListingApi.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel { Id = 1, Name = "Four Season", CountryId = 2, Rating = 4.5 },
                new Hotel { Id = 2, Name = "Cataract", CountryId = 1, Rating = 4.5 }
            );
        }
    }
}
