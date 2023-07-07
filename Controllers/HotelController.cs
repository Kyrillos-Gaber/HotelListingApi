using AutoMapper;
using HotelListingApi.Data;
using HotelListingApi.Data.IRepository;
using HotelListingApi.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListingApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<HotelController> logger;
        private readonly IMapper mapper;

        public HotelController(IUnitOfWork unitOfWork, ILogger<HotelController> logger, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllHotels()
        {
            try
            {
                IList<Hotel> hotels = await unitOfWork.HotelsRepo.GetAll();
                return Ok(mapper.Map<IList<HotelDto>>(hotels));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "CONGRATS!!! Internal Server Error");
            }
        }

        //[Authorize]
        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotel(int id)
        {
            try
            {
                Hotel hotel = await unitOfWork.HotelsRepo.Get(x => x.Id == id, new List<string> { "Country" });
                return Ok(mapper.Map<HotelDto>(hotel));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "CONGRATS!!! Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDto hotelDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(hotelDto);

            try
            {
                Hotel hotel = mapper.Map<Hotel>(hotelDto);
                await unitOfWork.HotelsRepo.Insert(hotel);
                await unitOfWork.Save();

                return CreatedAtRoute("GetHotel", new { Id = hotel.Id }, hotel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "Inrenal Server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelDto hotelDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(hotelDto);

            try
            {
                Hotel hotel = await unitOfWork.HotelsRepo.Get(x => x.Id == hotelDto.Id);
                if (hotel is null)
                    return BadRequest(new {error = "submitted data is invalid", data = hotelDto});

                mapper.Map(hotelDto, hotel);
                unitOfWork.HotelsRepo.Update(hotel);
                await unitOfWork.Save();

                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Error => " + ex);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await unitOfWork.HotelsRepo.Delete(id);
                await unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error => " + ex);
            }
        }

    }
}
