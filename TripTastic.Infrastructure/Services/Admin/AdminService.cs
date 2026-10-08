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
    public async Task<AdminDashboardResponse> GetDashboardAsync()
    {
        var dashboard = new AdminDashboardResponse
        {
            TotalCustomers = await _context.Customers.CountAsync(),

            TotalDrivers = await _context.Drivers.CountAsync(),
            ApprovedDrivers = await _context.Drivers
                .CountAsync(d => d.Status == "APPROVED"),
            PendingDrivers = await _context.Drivers
                .CountAsync(d => d.Status == "PENDING"),

            TotalVehicles = await _context.Vehicles.CountAsync(),
            ApprovedVehicles = await _context.Vehicles
                .CountAsync(v => v.Status == "APPROVED"),
            PendingVehicles = await _context.Vehicles
                .CountAsync(v => v.Status == "PENDING"),

            TotalBookings = await _context.Bookings.CountAsync(),
            PendingBookings = await _context.Bookings
                .CountAsync(b => b.Status == "PENDING"),
            AcceptedBookings = await _context.Bookings
                .CountAsync(b => b.Status == "ACCEPTED"),
            StartedBookings = await _context.Bookings
                .CountAsync(b => b.Status == "STARTED"),
            CompletedBookings = await _context.Bookings
                .CountAsync(b => b.Status == "COMPLETED"),
            RejectedBookings = await _context.Bookings
                .CountAsync(b => b.Status == "REJECTED"),

            TotalPayments = await _context.Payments.CountAsync(),

            TotalReviews = await _context.Reviews.CountAsync()
        };

        return dashboard;
    }

    public async Task<AdminReportResponse> GetReportAsync()
    {
        var report = new AdminReportResponse
        {
            TotalCustomers = await _context.Customers.CountAsync(),

            TotalDrivers = await _context.Drivers.CountAsync(),

            TotalVehicles = await _context.Vehicles.CountAsync(),

            TotalBookings = await _context.Bookings.CountAsync(),

            CompletedBookings = await _context.Bookings
                .CountAsync(b => b.Status == "COMPLETED"),

            RejectedBookings = await _context.Bookings
                .CountAsync(b => b.Status == "REJECTED"),

            TotalPayments = await _context.Payments.CountAsync(),

            TotalReviews = await _context.Reviews.CountAsync()
        };

        return report;
    }
}