namespace TripTastic.Application.DTOs.Driver;

public class DriverVehicleDetailsResponse
{
    public int DriverId { get; set; }
    public string DriverName { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public DateTime LicenseExpiryDate { get; set; }
    public int ExperienceYears { get; set; }
    public string? ProfilePhoto { get; set; }

    public int VehicleId { get; set; }
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