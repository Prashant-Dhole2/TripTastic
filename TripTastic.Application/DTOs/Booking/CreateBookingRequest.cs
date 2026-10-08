namespace TripTastic.Application.DTOs.Booking;

public class CreateBookingRequest
{
    public int CustomerId { get; set; }

    public int DriverId { get; set; }

    public int VehicleId { get; set; }

    public string PickupLocation { get; set; } = string.Empty;

    public string DropLocation { get; set; } = string.Empty;

    public DateTime TravelDate { get; set; }

    public int NumberOfPassengers { get; set; }
}