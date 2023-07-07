using HotelListingApi;
using HotelListingApi.Configurations;
using HotelListingApi.Data;
using HotelListingApi.Data.IRepository;
using HotelListingApi.Data.IRepository.Repository;
using HotelListingApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.AddCors( x => {
    x.AddPolicy("AllowAll", builder => 
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddSwaggerGen();

builder.Host.UseSerilog();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAutoMapper(typeof(MapperInitializer));

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAuthManager,  AuthManager>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.UseEndpoints(endpoints => endpoints.MapControllers());

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        path: @"D:\Hotel\logs\log-.txt",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Information)
    .CreateLogger();

try
{
    Log.Information("Application is Starting");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application is faild to Start");
}
finally
{
    Log.CloseAndFlush();
}
