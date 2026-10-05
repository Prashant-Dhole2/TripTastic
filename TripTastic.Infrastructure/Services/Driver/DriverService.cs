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
}