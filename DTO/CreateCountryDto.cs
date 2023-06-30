using System.ComponentModel.DataAnnotations;

namespace HotelListingApi.DTO
{
    public class CreateCountryDto
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "maximumLength is 50 chars")]
        public string? Name { get; set; }

        [Required]
        [StringLength(maximumLength: 10, ErrorMessage = "maximumLength is 10 chars")]
        public string? CountryCode { get; set; }
    }
}
