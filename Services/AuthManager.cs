using HotelListingApi.Data;
using HotelListingApi.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelListingApi.Services;

public class AuthManager : IAuthManager
{
    private readonly UserManager<ApiUser> userManager;
    private readonly IConfiguration configuration;
    private ApiUser? user;

    public AuthManager(UserManager<ApiUser> userManager, IConfiguration configuration)
    {
        this.userManager = userManager;
        this.configuration = configuration;
    }

    public async Task<string> CreateToken()
    {
        SigningCredentials singinCredentials = GetSigningCredentials();
        List<Claim> claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(singinCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials singinCredentials, List<Claim> claims)
    {
        IConfigurationSection settings = configuration.GetSection("Jwt");
        DateTime expire = DateTime.Now.AddDays(Convert.ToDouble(settings.GetSection("lifetime").Value!));
        
        JwtSecurityToken token = new (
            issuer: settings.GetSection("validIssuer").Value,
            claims: claims,
            expires: expire,
            signingCredentials: singinCredentials
        );

        return token;
    }

    private async Task<List<Claim>> GetClaims()
    {
        List<Claim> claims = new () { new Claim(ClaimTypes.Name, user!.Email) };
        IList<string> roles = await userManager.GetRolesAsync(user);

        foreach (string role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    private SigningCredentials GetSigningCredentials()
    {
        string key = configuration.GetSection("Jwt").GetSection("Key").ToString()!;
        SymmetricSecurityKey secret = new (Encoding.UTF8.GetBytes(key));

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    public async Task<bool> ValidateUser(UserLoginDto userDto)
    {
        user = await userManager.FindByEmailAsync(userDto.Email);
        return (user != null && await userManager.CheckPasswordAsync(user, userDto.Password));
    }
}
