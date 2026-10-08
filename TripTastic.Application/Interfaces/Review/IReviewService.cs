using TripTastic.Application.DTOs.Review;

namespace TripTastic.Application.Interfaces.Review;

public interface IReviewService
{
    Task<int?> CreateReviewAsync(
        CreateReviewRequest request);
}