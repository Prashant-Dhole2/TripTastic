using TripTastic.Application.DTOs.Driver;

namespace TripTastic.Application.Interfaces.Driver;

public interface IDriverService
{
    Task<bool> RegisterDriverAsync(RegisterDriverRequest request);

    Task<List<SearchAvailableDriverResponse>> SearchAvailableDriversAsync(
     string? vehicleType);

    Task<DriverVehicleDetailsResponse?> GetDriverVehicleDetailsAsync(
    int driverId,
    int vehicleId);

}


