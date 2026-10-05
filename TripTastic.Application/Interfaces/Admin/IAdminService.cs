using TripTastic.Application.DTOs.Admin;

namespace TripTastic.Application.Interfaces.Admin;

public interface IAdminService
{
    Task<bool> VerifyDriverAsync(int driverId, VerifyDriverRequest request);
    Task<bool> VerifyVehicleAsync(int vehicleId, VerifyVehicleRequest request);
}