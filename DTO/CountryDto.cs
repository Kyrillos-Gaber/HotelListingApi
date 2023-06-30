using HotelListingApi.Data;
using System.ComponentModel.DataAnnotations;

namespace HotelListingApi.DTO
{
    public class CountryDto : CreateCountryDto
    {
        public int Id { get; set; }

        public IList<Hotel>? Hotels { get; set; }

    }
}
