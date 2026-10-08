using Microsoft.AspNetCore.Mvc;
using TripTastic.Application.DTOs.Booking;
using TripTastic.Application.Interfaces.Booking;

namespace TripTastic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateBooking(
        CreateBookingRequest request)
    {
        var bookingId = await _bookingService
     .CreateBookingAsync(request);

        if (bookingId == null)
        {
            return BadRequest(new
            {
                message = "Customer, approved driver, or approved vehicle not found."
            });
        }

        return StatusCode(201, new
        {
            message = "Booking created successfully.",
            bookingId = bookingId
        });
    }

    [HttpPut("{bookingId}/accept")]
    public async Task<IActionResult> AcceptBooking(
    int bookingId,
    AcceptBookingRequest request)
    {
        var acceptedBookingId = await _bookingService
    .AcceptBookingAsync(bookingId, request);

        if (acceptedBookingId == null)
        {
            return BadRequest(new
            {
                message = "Booking cannot be accepted."
            });
        }

        return Ok(new
        {
            message = "Booking accepted successfully.",
            bookingId = acceptedBookingId
        });
    }
}
