using Microsoft.EntityFrameworkCore;
using TripTastic.Application.DTOs.Review;
using TripTastic.Application.Interfaces.Review;
using TripTastic.Infrastructure.Data;
using ReviewEntity = TripTastic.Domain.Entities.Review;

namespace TripTastic.Infrastructure.Services.Review;

public class ReviewService : IReviewService
{
    private readonly ApplicationDbContext _context;

    public ReviewService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int?> CreateReviewAsync(
        CreateReviewRequest request)
    {
        // 1. Check booking exists
        var booking = await _context.Bookings
            .FirstOrDefaultAsync(b => b.Id == request.BookingId);

        if (booking == null)
        {
            return null;
        }

        // 2. Booking must be completed
        if (booking.Status != "COMPLETED")
        {
            return null;
        }

        // 3. Customer must belong to the booking
        if (booking.CustomerId != request.CustomerId)
        {
            return null;
        }

        // 4. Rating must be between 1 and 5
        if (request.Rating < 1 || request.Rating > 5)
        {
            return null;
        }

        // 5. Check whether review already exists
        var existingReview = await _context.Reviews
            .FirstOrDefaultAsync(r => r.BookingId == request.BookingId);

        if (existingReview != null)
        {
            return null;
        }

        // 6. Create review
        var review = new ReviewEntity
        {
            BookingId = booking.Id,
            CustomerId = booking.CustomerId,
            DriverId = booking.DriverId,
            Rating = request.Rating,
            Comment = request.Comment,
            CreatedAt = DateTime.UtcNow
        };

        _context.Reviews.Add(review);

        // 7. Save to database
        await _context.SaveChangesAsync();

        // 8. Return generated review ID
        return review.Id;
    }
}