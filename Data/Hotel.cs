using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListingApi.Data
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public sbyte? Address { get; set; }

        public double Rating { get; set; }

        [ForeignKey(nameof(Country))]
        [Required]
        public int CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
