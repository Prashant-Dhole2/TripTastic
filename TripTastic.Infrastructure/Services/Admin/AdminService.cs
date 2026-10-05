using Microsoft.EntityFrameworkCore;
using TripTastic.Application.DTOs.Admin;
using TripTastic.Application.Interfaces.Admin;
using TripTastic.Infrastructure.Data;

namespace TripTastic.Infrastructure.Services.Admin;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext _context;

    public AdminService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> VerifyDriverAsync(
        int driverId,
        VerifyDriverRequest request)
    {
        var driver = await _context.Drivers
            .FirstOrDefaultAsync(x => x.Id == driverId);

        if (driver == null)
            return false;

        if (request.Status != "APPROVED" &&
            request.Status != "REJECTED")
            return false;

        driver.Status = request.Status;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> VerifyVehicleAsync(
        int vehicleId,
        VerifyVehicleRequest request)
    {
        var vehicle = await _context.Vehicles
            .FirstOrDefaultAsync(x => x.Id == vehicleId);

        if (vehicle == null)
            return false;

        if (request.Status != "APPROVED" &&
            request.Status != "REJECTED")
            return false;

        vehicle.Status = request.Status;

        await _context.SaveChangesAsync();

        return true;
    }
}