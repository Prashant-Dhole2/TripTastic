using TripTastic.Application.DTOs.Payment;

namespace TripTastic.Application.Interfaces.Payment;

public interface IPaymentService
{
    Task<int?> CreatePaymentAsync(
        CreatePaymentRequest request);
}