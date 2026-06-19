using BusTicketManagement.Application.DTOs.BusDTOs;
using BusTicketManagement.Application.Interfaces;
using BusTicketManagement.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BusController : ControllerBase
{
    private readonly IBusService _busService;
    private readonly ILogger<BusController> _logger;

    public BusController(IBusService busService, ILogger<BusController> logger)
    {
        _busService = busService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<BusDto>>>> GetAllBuses()
    {
        try
        {
            var buses = await _busService.GetAllBusesAsync();
            return Ok(ApiResponse<List<BusDto>>.SuccessResponse(buses, "Buses retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving buses");
            return BadRequest(ApiResponse<List<BusDto>>.FailureResponse(ex.Message));
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<BusDto>>> GetBusById(int id)
    {
        try
        {
            var bus = await _busService.GetBusByIdAsync(id);
            if (bus == null)
                return NotFound(ApiResponse<BusDto>.FailureResponse("Bus not found"));

            return Ok(ApiResponse<BusDto>.SuccessResponse(bus, "Bus retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving bus");
            return BadRequest(ApiResponse<BusDto>.FailureResponse(ex.Message));
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<BusDto>>> CreateBus([FromBody] CreateBusDto createBusDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<BusDto>.FailureResponse("Invalid bus data"));

            var bus = await _busService.CreateBusAsync(createBusDto);
            return CreatedAtAction(nameof(GetBusById), new { id = bus.BusId }, ApiResponse<BusDto>.SuccessResponse(bus, "Bus created successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating bus");
            return BadRequest(ApiResponse<BusDto>.FailureResponse(ex.Message));
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<BusDto>>> UpdateBus(int id, [FromBody] UpdateBusDto updateBusDto)
    {
        try
        {
            updateBusDto.BusId = id;
            var bus = await _busService.UpdateBusAsync(updateBusDto);
            return Ok(ApiResponse<BusDto>.SuccessResponse(bus, "Bus updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating bus");
            return BadRequest(ApiResponse<BusDto>.FailureResponse(ex.Message));
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteBus(int id)
    {
        try
        {
            var result = await _busService.DeleteBusAsync(id);
            return Ok(ApiResponse<bool>.SuccessResponse(result, "Bus deleted successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting bus");
            return BadRequest(ApiResponse<bool>.FailureResponse(ex.Message));
        }
    }
}
