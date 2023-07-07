using AutoMapper;
using HotelListingApi.Data;
using HotelListingApi.DTO;
using HotelListingApi.Services;
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
    private readonly IAuthManager authManager;
    private readonly ILogger<AccountController> logger;
    private readonly IMapper mapper;

    public AccountController(UserManager<ApiUser> userManager,
        IAuthManager authManager, 
        ILogger<AccountController> logger,
        IMapper mapper)
    {
        this.userManager = userManager;
        this.authManager = authManager;
        this.logger = logger;
        this.mapper = mapper;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] UserDto userDto)
    {
        logger.LogInformation($"Registration Attempt for {userDto.Email}");
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            ApiUser user = mapper.Map<ApiUser>(userDto);
            IdentityResult res = await userManager.CreateAsync(user, userDto.Password);
            if(!res.Succeeded)
            {
                foreach (var er in res.Errors)
                    ModelState.AddModelError(er.Code, er.Description);
                return BadRequest(ModelState);
            }

            await userManager.AddToRolesAsync(user, userDto.Roles);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Somthing Wrong in {nameof(Register)}");
            return Problem($"Something Went Wron {nameof(Register)}", statusCode:500);
        }
    }
    
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] UserLoginDto user)
    {
        logger.LogInformation($"Registration Attempt for {user.Email}");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            if (!await authManager.ValidateUser(user))
            {
                return Unauthorized(user);
            }

            return Ok(new { Token = await authManager.CreateToken() });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Somthing Wrong in {nameof(Login)} ex message is => {ex.Message}");
            return Problem($"Something Went Wron {nameof(Login)}", statusCode: 500);
        }
    }
    
}
