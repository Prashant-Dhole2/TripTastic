using TripTastic.Application.DTOs.Booking;

namespace TripTastic.Application.Interfaces.Booking;

public interface IBookingService
{
    Task<bool> CreateBookingAsync(CreateBookingRequest request);
}