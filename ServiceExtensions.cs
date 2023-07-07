using HotelListingApi.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HotelListingApi;

public static class ServiceExtensions
{
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        IdentityBuilder builder = services.AddIdentityCore<ApiUser>(
            identityOptions => identityOptions.User.RequireUniqueEmail = true);

        builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);

        builder.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
    }

    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection jwtSettings = configuration.GetSection("Jwt");
        string Key = jwtSettings.GetSection("Key").ToString()!;

        services.AddAuthentication(authenticationOptions =>
        {
            authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwtOptions =>
        {
            jwtOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)),
                ValidAudience = "localhost"
            };
        });
    }
}
