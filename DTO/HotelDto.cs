using HotelListingApi.Data;
using System.ComponentModel.DataAnnotations;

namespace HotelListingApi.DTO
{
    public class HotelDto : CreateHotelDto
    {
        public int Id { get; set; }

        public Country? Country { get; set; }
    }
}
