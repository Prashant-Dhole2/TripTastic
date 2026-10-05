using Microsoft.EntityFrameworkCore;
using TripTastic.Application.DTOs.Vehicle;
using TripTastic.Application.Interfaces.Vehicle;
using TripTastic.Domain.Entities;
using TripTastic.Infrastructure.Data;

namespace TripTastic.Infrastructure.Services.Vehicle;

public class VehicleService : IVehicleService
{
    private readonly ApplicationDbContext _context;

    public VehicleService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> RegisterVehicleAsync(
        RegisterVehicleRequest request)
    {
        var driver = await _context.Drivers
            .FirstOrDefaultAsync(x => x.Id == request.DriverId);

        if (driver == null)
        {
            return false;
        }

        var existingVehicle = await _context.Vehicles
            .FirstOrDefaultAsync(x =>
                x.VehicleNumber == request.VehicleNumber);

        if (existingVehicle != null)
        {
            return false;
        }

        var vehicle = new Domain.Entities.Vehicle
        {
            DriverId = request.DriverId,
            VehicleNumber = request.VehicleNumber,
            VehicleType = request.VehicleType,
            Brand = request.Brand,
            Model = request.Model,
            ManufacturingYear = request.ManufacturingYear,
            Color = request.Color,
            SeatingCapacity = request.SeatingCapacity,
            IsAC = request.IsAC,
            VehiclePhoto = request.VehiclePhoto,
            Status = "PENDING"
        };

        _context.Vehicles.Add(vehicle);

        await _context.SaveChangesAsync();

        return true;
    }
}