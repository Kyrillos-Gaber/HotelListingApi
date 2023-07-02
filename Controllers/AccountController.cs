using AutoMapper;
using HotelListingApi.Data;
using HotelListingApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace HotelListingApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApiUser> userManager;
    //private readonly SignInManager<ApiUser> signInManager;
    private readonly ILogger<AccountController> logger;
    private readonly IMapper mapper;

    public AccountController(UserManager<ApiUser> userManager, 
        //SignInManager<ApiUser> signInManager, 
        ILogger<AccountController> logger,
        IMapper mapper)
    {
        this.userManager = userManager;
        //this.signInManager = signInManager;
        this.logger = logger;
        this.mapper = mapper;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Register([FromBody] UserDto user)
    {
        logger.LogInformation($"Registration Attempt for {user.Email}");
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            ApiUser userX = mapper.Map<ApiUser>(user);
            IdentityResult res = await userManager.CreateAsync(userX);
            if(!res.Succeeded)
            {
                foreach (var er in res.Errors)
                    ModelState.AddModelError(er.Code, er.Description);
                return BadRequest(ModelState);
            }
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Somthing Wrong in {nameof(Register)}");
            return Problem($"Something Went Wron {nameof(Register)}", statusCode:500);
        }
    }
    /*
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Login([FromBody] UserLoginDto user)
    {
        logger.LogInformation($"Registration Attempt for {user.Email}");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            SignInResult result = await signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);

            if (!result.Succeeded)
            {
                return Unauthorized(user);
            }

            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Somthing Wrong in {nameof(Login)}");
            return Problem($"Something Went Wron {nameof(Login)}", statusCode: 500);
        }
    }
    */
}
