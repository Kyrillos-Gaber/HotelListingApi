using System.ComponentModel.DataAnnotations;

namespace HotelListingApi.DTO
{
    public class CreateHotelDto
    {
        [Required]
        public string? Name { get; set; }

        public sbyte? Address { get; set; }

        [Range(0, 10)]
        public double Rating { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}
