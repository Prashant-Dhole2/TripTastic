using Microsoft.EntityFrameworkCore;
using TripTastic.Application.DTOs.Driver;
using TripTastic.Application.Interfaces.Driver;
using TripTastic.Domain.Entities;
using TripTastic.Infrastructure.Data;

namespace TripTastic.Infrastructure.Services.Driver;

public class DriverService : IDriverService
{
    private readonly ApplicationDbContext _context;

    public DriverService(ApplicationDbContext context)
    {
        _context = context;
    }

    // =========================================================
    // TT-006: Register Driver
    // =========================================================
    public async Task<bool> RegisterDriverAsync(
        RegisterDriverRequest request)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email);

        if (existingUser != null)
        {
            return false;
        }

        var existingLicense = await _context.Drivers
            .FirstOrDefaultAsync(x =>
                x.LicenseNumber == request.LicenseNumber);

        if (existingLicense != null)
        {
            return false;
        }

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,

            // Temporary for basic registration flow.
            // Password hashing will be added later.
            PasswordHash = request.Password,

            Role = "DRIVER",
            IsActive = true
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        var driver = new Domain.Entities.Driver
        {
            UserId = user.Id,
            LicenseNumber = request.LicenseNumber,
            LicenseExpiryDate = request.LicenseExpiryDate,
            ExperienceYears = request.ExperienceYears,
            ProfilePhoto = request.ProfilePhoto,
            Status = "PENDING"
        };

        _context.Drivers.Add(driver);

        await _context.SaveChangesAsync();

        return true;
    }

    // =========================================================
    // TT-010: Search Available Drivers
    // =========================================================
    public async Task<List<SearchAvailableDriverResponse>>
        SearchAvailableDriversAsync(string? vehicleType)
    {
        var drivers = await _context.Drivers
            .Include(d => d.User)
            .Where(d => d.Status == "APPROVED")
            .Join(
                _context.Vehicles
                    .Where(v => v.Status == "APPROVED")
                    .Where(v =>
                        string.IsNullOrEmpty(vehicleType) ||
                        v.VehicleType == vehicleType),

                driver => driver.Id,
                vehicle => vehicle.DriverId,

                (driver, vehicle) =>
                    new SearchAvailableDriverResponse
                    {
                        DriverId = driver.Id,

                        DriverName =
                            driver.User.FirstName + " " +
                            driver.User.LastName,

                        VehicleId = vehicle.Id,
                        VehicleNumber = vehicle.VehicleNumber,
                        VehicleType = vehicle.VehicleType,
                        Brand = vehicle.Brand,
                        Model = vehicle.Model,
                        SeatingCapacity = vehicle.SeatingCapacity,
                        IsAC = vehicle.IsAC
                    })
            .ToListAsync();

        return drivers;
    }

    // =========================================================
    // TT-011: Get Driver & Vehicle Details
    // =========================================================
    public async Task<DriverVehicleDetailsResponse?>
    GetDriverVehicleDetailsAsync(
        int driverId,
        int vehicleId)
    {
        var driver = await _context.Drivers
            .Include(d => d.User)
            .Include(d => d.Vehicles)
            .FirstOrDefaultAsync(d =>
                d.Id == driverId &&
                d.Status == "APPROVED");

        if (driver == null)
        {
            return null;
        }

        var vehicle = driver.Vehicles
            .FirstOrDefault(v =>
                v.Id == vehicleId &&
                v.Status == "APPROVED");

        if (vehicle == null)
        {
            return null;
        }

        return new DriverVehicleDetailsResponse
        {
            DriverId = driver.Id,

            DriverName =
                driver.User.FirstName + " " +
                driver.User.LastName,

            LicenseNumber = driver.LicenseNumber,
            LicenseExpiryDate = driver.LicenseExpiryDate,
            ExperienceYears = driver.ExperienceYears,
            ProfilePhoto = driver.ProfilePhoto,

            VehicleId = vehicle.Id,
            VehicleNumber = vehicle.VehicleNumber,
            VehicleType = vehicle.VehicleType,
            Brand = vehicle.Brand,
            Model = vehicle.Model,
            ManufacturingYear = vehicle.ManufacturingYear,
            Color = vehicle.Color,
            SeatingCapacity = vehicle.SeatingCapacity,
            IsAC = vehicle.IsAC,
            VehiclePhoto = vehicle.VehiclePhoto
        };
    }
}
