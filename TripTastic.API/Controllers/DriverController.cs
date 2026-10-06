using Microsoft.AspNetCore.Mvc;
using TripTastic.Application.DTOs.Driver;
using TripTastic.Application.Interfaces.Driver;

namespace TripTastic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriverController : ControllerBase
{
    private readonly IDriverService _driverService;

    public DriverController(IDriverService driverService)
    {
        _driverService = driverService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterDriver(
        RegisterDriverRequest request)
    {
        var result = await _driverService.RegisterDriverAsync(request);

        if (!result)
        {
            return BadRequest(
                "Email or license number already exists.");
        }

        return Ok("Driver registered successfully.");
    }

    [HttpGet("available")]
    public async Task<IActionResult> SearchAvailableDrivers(
    string? vehicleType)
    {
        var drivers = await _driverService
            .SearchAvailableDriversAsync(vehicleType);

        return Ok(drivers);
    }
    [HttpGet("{driverId}/details")]
    public async Task<IActionResult> GetDriverVehicleDetails(
    int driverId)
    {
        var driver = await _driverService
            .GetDriverVehicleDetailsAsync(driverId);

        if (driver == null)
        {
            return NotFound(new
            {
                message = "Approved driver or vehicle not found."
            });
        }

        return Ok(driver);
    }
}



