using TripTastic.Application.DTOs.Booking;

namespace TripTastic.Application.Interfaces.Booking;

public interface IBookingService
{
    Task<int?> CreateBookingAsync(CreateBookingRequest request);

    Task<int?> AcceptBookingAsync(
      int bookingId,
      AcceptBookingRequest request);

    Task<int?> RejectBookingAsync(
    int bookingId,
    RejectBookingRequest request);
}