namespace TripTastic.Application.DTOs.Vehicle;

public class RegisterVehicleRequest
{
    public int DriverId { get; set; }

    public string VehicleNumber { get; set; } = string.Empty;

    public string VehicleType { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public int ManufacturingYear { get; set; }

    public string Color { get; set; } = string.Empty;

    public int SeatingCapacity { get; set; }

    public bool IsAC { get; set; }

    public string? VehiclePhoto { get; set; }
}