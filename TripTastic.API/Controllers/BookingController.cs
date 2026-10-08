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

    [HttpPut("{bookingId}/reject")]
    public async Task<IActionResult> RejectBooking(
    int bookingId,
    RejectBookingRequest request)
    {
        var rejectedBookingId = await _bookingService
            .RejectBookingAsync(bookingId, request);

        if (rejectedBookingId == null)
        {
            return BadRequest(new
            {
                message = "Booking cannot be rejected."
            });
        }

        return Ok(new
        {
            message = "Booking rejected successfully.",
            bookingId = rejectedBookingId
        });
    }

    [HttpPut("{bookingId}/start")]
    public async Task<IActionResult> StartTrip(
    int bookingId,
    StartTripRequest request)
    {
        var startedBookingId = await _bookingService
            .StartTripAsync(bookingId, request);

        if (startedBookingId == null)
        {
            return BadRequest(new
            {
                message = "Trip cannot be started."
            });
        }

        return Ok(new
        {
            message = "Trip started successfully.",
            bookingId = startedBookingId
        });
    }

    [HttpPut("{bookingId}/complete")]
    public async Task<IActionResult> CompleteTrip(
    int bookingId,
    CompleteBookingRequest request)
    {
        var completedBookingId = await _bookingService
            .CompleteTripAsync(bookingId, request);

        if (completedBookingId == null)
        {
            return BadRequest(new
            {
                message = "Trip cannot be completed."
            });
        }

        return Ok(new
        {
            message = "Trip completed successfully.",
            bookingId = completedBookingId
        });
    }
}
