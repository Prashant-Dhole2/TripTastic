using TripTastic.Application.DTOs.Vehicle;

namespace TripTastic.Application.Interfaces.Vehicle;

public interface IVehicleService
{
    Task<bool> RegisterVehicleAsync(
        RegisterVehicleRequest request);
}   