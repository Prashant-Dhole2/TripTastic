using TripTastic.Application.DTOs.Auth;

namespace TripTastic.Application.Interfaces.Auth;

public interface IAuthService
{
    Task<bool> RegisterCustomerAsync(
        RegisterCustomerRequest request);
}