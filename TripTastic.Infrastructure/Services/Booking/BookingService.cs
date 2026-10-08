using Microsoft.EntityFrameworkCore;
using TripTastic.Application.DTOs.Booking;
using TripTastic.Application.Interfaces.Booking;
using BookingEntity = TripTastic.Domain.Entities.Booking;
using TripTastic.Infrastructure.Data;

namespace TripTastic.Infrastructure.Services.Booking;

public class BookingService : IBookingService
{
    private readonly ApplicationDbContext _context;

    public BookingService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int?> CreateBookingAsync(
     CreateBookingRequest request)
    {
        // Check customer exists
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == request.CustomerId);

        if (customer == null)
        {
            return null;
        }

        // Check driver is approved
        var driver = await _context.Drivers
            .FirstOrDefaultAsync(d =>
                d.Id == request.DriverId &&
                d.Status == "APPROVED");

        if (driver == null)
        {
            return null;
        }

        // Check vehicle belongs to driver and is approved
        var vehicle = await _context.Vehicles
            .FirstOrDefaultAsync(v =>
                v.Id == request.VehicleId &&
                v.DriverId == request.DriverId &&
                v.Status == "APPROVED");

        if (vehicle == null)
        {
            return null;
        }

        // Create booking
        var booking = new BookingEntity
        {
            CustomerId = request.CustomerId,
            DriverId = request.DriverId,
            VehicleId = request.VehicleId,
            PickupLocation = request.PickupLocation,
            DropLocation = request.DropLocation,
            TravelDate = request.TravelDate,
            NumberOfPassengers = request.NumberOfPassengers,
            Status = "PENDING",
            CreatedAt = DateTime.UtcNow
        };

        _context.Bookings.Add(booking);

        await _context.SaveChangesAsync();

        return booking.Id;
    }
    public async Task<bool> AcceptBookingAsync(
    int bookingId,
    AcceptBookingRequest request)
    {
        var booking = await _context.Bookings
            .FirstOrDefaultAsync(b => b.Id == bookingId);

        if (booking == null)
        {
            return false;
        }

        if (booking.DriverId != request.DriverId)
        {
            return false;
        }

        if (booking.Status != "PENDING")
        {
            return false;
        }

        booking.Status = "ACCEPTED";

        await _context.SaveChangesAsync();

        return true;
    }
}