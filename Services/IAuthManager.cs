using HotelListingApi.DTO;

namespace HotelListingApi.Services;

public interface IAuthManager
{
    Task<bool> ValidateUser(UserLoginDto userDto);

    Task<string> CreateToken();
}
