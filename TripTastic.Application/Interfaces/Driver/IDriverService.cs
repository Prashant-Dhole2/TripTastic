using TripTastic.Application.DTOs.Driver;

namespace TripTastic.Application.Interfaces.Driver;

public interface IDriverService
{
    Task<bool> RegisterDriverAsync(
        RegisterDriverRequest request);
}