using Microsoft.AspNetCore.Mvc;
using TripTastic.Application.DTOs.Admin;
using TripTastic.Application.Interfaces.Admin;

namespace TripTastic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    
    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        var dashboard = await _adminService
            .GetDashboardAsync();

        return Ok(dashboard);
    }

    [HttpPut("drivers/{driverId}/verify")]
    public async Task<IActionResult> VerifyDriver(
        int driverId,
        VerifyDriverRequest request)
    {
        var result = await _adminService
            .VerifyDriverAsync(driverId, request);

        if (!result)
            return BadRequest(
                "Driver not found or invalid status.");

        return Ok("Driver status updated successfully.");
    }

    [HttpPut("vehicles/{vehicleId}/verify")]
    public async Task<IActionResult> VerifyVehicle(
        int vehicleId,
        VerifyVehicleRequest request)
    {
        var result = await _adminService
            .VerifyVehicleAsync(vehicleId, request);

        if (!result)
            return BadRequest(
                "Vehicle not found or invalid status.");

        return Ok("Vehicle status updated successfully.");
    }
}