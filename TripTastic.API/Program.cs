using Microsoft.EntityFrameworkCore;
using TripTastic.Application.Interfaces.Auth;
using TripTastic.Application.Interfaces.Driver;
using TripTastic.Application.Interfaces.Vehicle;
using TripTastic.Infrastructure.Data;
using TripTastic.Infrastructure.Services.Auth;
using TripTastic.Infrastructure.Services.Driver;
using TripTastic.Infrastructure.Services.Vehicle;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
