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
        var result = await _bookingService
            .CreateBookingAsync(request);

        if (!result)
        {
            return BadRequest(new
            {
                message = "Customer, approved driver, or approved vehicle not found."
            });
        }

        return Ok(new
        {
            message = "Booking created successfully."
        });
    }
}