namespace TripTastic.Application.DTOs.Driver;

public class SearchAvailableDriverResponse
{
    public int DriverId { get; set; }
    public string DriverName { get; set; } = string.Empty;

    public int VehicleId { get; set; }
    public string VehicleNumber { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int SeatingCapacity { get; set; }
    public bool IsAC { get; set; } 
}

