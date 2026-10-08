namespace TripTastic.Domain.Entities;

public class Booking
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int DriverId { get; set; }

    public int VehicleId { get; set; }

    public string PickupLocation { get; set; } = string.Empty;

    public string DropLocation { get; set; } = string.Empty;

    public DateTime TravelDate { get; set; }

    public int NumberOfPassengers { get; set; }

    public string Status { get; set; } = "PENDING";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Customer Customer { get; set; } = null!;

    public Driver Driver { get; set; } = null!;

    public Vehicle Vehicle { get; set; } = null!;
}