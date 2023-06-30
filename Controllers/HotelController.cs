using AutoMapper;
using HotelListingApi.Data;
using HotelListingApi.Data.IRepository;
using HotelListingApi.DTO;
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

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotel(int id)
        {
            try
            {
                Hotel hotel = await unitOfWork.HotelsRepo.Get(x => x.Id == id, new List<string> { "Country" } );
                return Ok(mapper.Map<HotelDto>(hotel));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "CONGRATS!!! Internal Server Error");
            }
        }

    }
}
