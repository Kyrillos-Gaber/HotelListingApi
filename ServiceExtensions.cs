using HotelListingApi.Data;
using Microsoft.AspNetCore.Identity;

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
}
