using Microsoft.AspNetCore.Mvc;
using TripTastic.Application.DTOs.Auth;
using TripTastic.Application.Interfaces.Auth;

namespace TripTastic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register-customer")]
    public async Task<IActionResult> RegisterCustomer(
        RegisterCustomerRequest request)
    {
        var result = await _authService.RegisterCustomerAsync(request);

        if (!result)
        {
            return BadRequest("Email already exists.");
        }

        return Ok("Customer registered successfully.");
    }
}