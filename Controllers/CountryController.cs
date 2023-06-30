using AutoMapper;
using HotelListingApi.Data.IRepository;
using HotelListingApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HotelListingApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CountryController> logger;
        private readonly IMapper mapper;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await unitOfWork.CountriesRepo.GetAll();
                return Ok( mapper.Map<IList<CountryDto>>(countries) );
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
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var country = await unitOfWork.CountriesRepo.Get(x => x.Id == id, new List<string> { "Hotels" } );
                return Ok(mapper.Map<CountryDto>(country));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "CONGRATS!!! Internal Server Error");
            }
        }


    }
}
