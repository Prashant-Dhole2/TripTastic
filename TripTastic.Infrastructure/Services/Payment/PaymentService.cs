using Microsoft.EntityFrameworkCore;
using TripTastic.Application.DTOs.Payment;
using TripTastic.Application.Interfaces.Payment;
using PaymentEntity = TripTastic.Domain.Entities.Payment;
using TripTastic.Infrastructure.Data;

namespace TripTastic.Infrastructure.Services.Payment;

public class PaymentService : IPaymentService
{
    private readonly ApplicationDbContext _context;

    public PaymentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int?> CreatePaymentAsync(
        CreatePaymentRequest request)
    {
        var booking = await _context.Bookings
            .FirstOrDefaultAsync(b => b.Id == request.BookingId);

        if (booking == null)
        {
            return null;
        }

        if (booking.Status != "COMPLETED")
        {
            return null;
        }

        var existingPayment = await _context.Payments
            .FirstOrDefaultAsync(p => p.BookingId == request.BookingId);

        if (existingPayment != null)
        {
            return null;
        }

        if (request.Amount <= 0)
        {
            return null;
        }

        var payment = new PaymentEntity
        {
            BookingId = request.BookingId,
            Amount = request.Amount,
            PaymentMethod = request.PaymentMethod,
            Status = "PAID",
            PaidAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.Payments.Add(payment);

        await _context.SaveChangesAsync();

        return payment.Id;
    }
}