using System.ComponentModel.DataAnnotations;

namespace HotelListingApi.DTO;

public class UserDto : UserLoginDto
{
    [Required]
    public string? UserName { get; set; }

    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
        
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }
}
