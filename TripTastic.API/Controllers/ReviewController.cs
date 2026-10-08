using Microsoft.AspNetCore.Mvc;
using TripTastic.Application.DTOs.Review;
using TripTastic.Application.Interfaces.Review;

namespace TripTastic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateReview(
        CreateReviewRequest request)
    {
        var reviewId = await _reviewService
            .CreateReviewAsync(request);

        if (reviewId == null)
        {
            return BadRequest(new
            {
                message = "Review cannot be created."
            });
        }

        return StatusCode(201, new
        {
            message = "Review created successfully.",
            reviewId = reviewId
        });
    }
}