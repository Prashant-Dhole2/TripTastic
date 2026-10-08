using Microsoft.AspNetCore.Mvc;
using TripTastic.Application.DTOs.Payment;
using TripTastic.Application.Interfaces.Payment;

namespace TripTastic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePayment(
        CreatePaymentRequest request)
    {
        var paymentId = await _paymentService
            .CreatePaymentAsync(request);

        if (paymentId == null)
        {
            return BadRequest(new
            {
                message = "Payment cannot be created."
            });
        }

        return StatusCode(201, new
        {
            message = "Payment created successfully.",
            paymentId = paymentId
        });
    }
}