using TripTastic.Application.DTOs.Booking;

namespace TripTastic.Application.Interfaces.Booking;

public interface IBookingService
{
    Task<int?> CreateBookingAsync(CreateBookingRequest request);

    Task<bool> AcceptBookingAsync(
        int bookingId,
        AcceptBookingRequest request);
}