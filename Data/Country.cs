using System.ComponentModel.DataAnnotations;

namespace HotelListingApi.Data
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? CountryCode { get; set; }

        public virtual IList<Hotel>? Hotels { get; set; }
    }
}
