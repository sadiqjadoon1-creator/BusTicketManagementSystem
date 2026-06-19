using BusTicketManagement.Application.DTOs.RouteDTOs;
using BusTicketManagement.Application.Interfaces;
using BusTicketManagement.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RouteController : ControllerBase
{
    private readonly IRouteService _routeService;
    private readonly ILogger<RouteController> _logger;

    public RouteController(IRouteService routeService, ILogger<RouteController> logger)
    {
        _routeService = routeService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<RouteDto>>>> GetAllRoutes()
    {
        try
        {
            var routes = await _routeService.GetAllRoutesAsync();
            return Ok(ApiResponse<List<RouteDto>>.SuccessResponse(routes, "Routes retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving routes");
            return BadRequest(ApiResponse<List<RouteDto>>.FailureResponse(ex.Message));
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<RouteDto>>> GetRouteById(int id)
    {
        try
        {
            var route = await _routeService.GetRouteByIdAsync(id);
            return Ok(ApiResponse<RouteDto>.SuccessResponse(route, "Route retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving route");
            return BadRequest(ApiResponse<RouteDto>.FailureResponse(ex.Message));
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<RouteDto>>> CreateRoute([FromBody] CreateRouteDto createRouteDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<RouteDto>.FailureResponse("Invalid route data"));

            var route = await _routeService.CreateRouteAsync(createRouteDto);
            return CreatedAtAction(nameof(GetRouteById), new { id = route.RouteId }, ApiResponse<RouteDto>.SuccessResponse(route, "Route created successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating route");
            return BadRequest(ApiResponse<RouteDto>.FailureResponse(ex.Message));
        }
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<List<RouteDto>>>> SearchRoutes(int sourceCityId, int destinationCityId)
    {
        try
        {
            var routes = await _routeService.GetRoutesByCitiesAsync(sourceCityId, destinationCityId);
            return Ok(ApiResponse<List<RouteDto>>.SuccessResponse(routes, "Routes found"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching routes");
            return BadRequest(ApiResponse<List<RouteDto>>.FailureResponse(ex.Message));
        }
    }
}
