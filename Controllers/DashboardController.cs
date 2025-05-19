
using SchoolManagementSystem.Api.Dtos.Dashboard;
using SchoolManagementSystem.Api.Services.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementSystem.Api.Controllers;
[ApiController]
[Route("/dashboard")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }
    

    [HttpGet()]
    [Produces(typeof(DashboardDataDto))]
    public async Task<IActionResult> GetDashboardDataAsync()
    {
        try
        {
            var dashboardData = await _dashboardService.GetDashboardDataAsync();
            return Ok(dashboardData);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}

