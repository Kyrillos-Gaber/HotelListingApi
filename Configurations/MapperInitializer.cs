using AutoMapper;
using HotelListingApi.Data;
using HotelListingApi.DTO;

namespace HotelListingApi.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<Hotel, HotelDto>();
        }
    }
}
