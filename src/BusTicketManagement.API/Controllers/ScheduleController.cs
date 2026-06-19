using BusTicketManagement.Application.DTOs.ScheduleDTOs;
using BusTicketManagement.Application.Interfaces;
using BusTicketManagement.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleService _scheduleService;
    private readonly ILogger<ScheduleController> _logger;

    public ScheduleController(IScheduleService scheduleService, ILogger<ScheduleController> logger)
    {
        _scheduleService = scheduleService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ScheduleDto>>> GetScheduleById(int id)
    {
        try
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            return Ok(ApiResponse<ScheduleDto>.SuccessResponse(schedule, "Schedule retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving schedule");
            return BadRequest(ApiResponse<ScheduleDto>.FailureResponse(ex.Message));
        }
    }

    [HttpGet("route/{routeId}")]
    public async Task<ActionResult<ApiResponse<List<ScheduleDto>>>> GetSchedulesByRoute(int routeId, [FromQuery] DateTime date)
    {
        try
        {
            var schedules = await _scheduleService.GetSchedulesByRouteAsync(routeId, date);
            return Ok(ApiResponse<List<ScheduleDto>>.SuccessResponse(schedules, "Schedules retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving schedules");
            return BadRequest(ApiResponse<List<ScheduleDto>>.FailureResponse(ex.Message));
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<ScheduleDto>>> CreateSchedule([FromBody] CreateScheduleDto createScheduleDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ScheduleDto>.FailureResponse("Invalid schedule data"));

            var schedule = await _scheduleService.CreateScheduleAsync(createScheduleDto);
            return CreatedAtAction(nameof(GetScheduleById), new { id = schedule.ScheduleId }, ApiResponse<ScheduleDto>.SuccessResponse(schedule, "Schedule created successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating schedule");
            return BadRequest(ApiResponse<ScheduleDto>.FailureResponse(ex.Message));
        }
    }
}
