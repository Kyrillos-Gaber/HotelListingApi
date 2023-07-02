using AutoMapper;
using HotelListingApi.Data;
using HotelListingApi.DTO;

namespace HotelListingApi.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, CreateHotelDto>().ReverseMap();
            CreateMap<Hotel, HotelDto>().ReverseMap();
            CreateMap<Hotel, CreateHotelDto>().ReverseMap();
            CreateMap<ApiUser, UserDto>().ReverseMap();
        }
    }
}
