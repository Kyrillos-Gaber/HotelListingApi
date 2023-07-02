using System.ComponentModel.DataAnnotations;

namespace HotelListingApi.DTO;

public class UserLoginDto
{
    [Required]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
