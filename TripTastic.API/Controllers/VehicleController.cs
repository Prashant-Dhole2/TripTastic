using Microsoft.AspNetCore.Mvc;
using TripTastic.Application.DTOs.Vehicle;
using TripTastic.Application.Interfaces.Vehicle;

namespace TripTastic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterVehicle(RegisterVehicleRequest request)
    {
        var result = await _vehicleService.RegisterVehicleAsync(request);

        if (!result)
            return BadRequest("Driver not found or vehicle number already exists.");

        return Ok("Vehicle registered successfully.");
    }
}