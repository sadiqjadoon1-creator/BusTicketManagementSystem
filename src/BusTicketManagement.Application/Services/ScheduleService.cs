using BusTicketManagement.Application.DTOs.ScheduleDTOs;
using BusTicketManagement.Application.Interfaces;
using BusTicketManagement.Domain.Entities;
using BusTicketManagement.Infrastructure.Repository;
using AutoMapper;

namespace BusTicketManagement.Application.Services;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ScheduleService> _logger;

    public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper, ILogger<ScheduleService> logger)
    {
        _scheduleRepository = scheduleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<ScheduleDto>> GetSchedulesByRouteAsync(int routeId, DateTime date)
    {
        try
        {
            _logger.LogInformation($"Fetching schedules for route {routeId} on {date.Date}");
            var schedules = await _scheduleRepository.GetByRouteAndDateAsync(routeId, date);
            return _mapper.Map<List<ScheduleDto>>(schedules);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching schedules");
            throw;
        }
    }

    public async Task<ScheduleDto> GetScheduleByIdAsync(int scheduleId)
    {
        try
        {
            _logger.LogInformation($"Fetching schedule with ID: {scheduleId}");
            var schedule = await _scheduleRepository.GetByIdAsync(scheduleId);
            if (schedule == null)
                throw new InvalidOperationException($"Schedule with ID {scheduleId} not found");
            
            return _mapper.Map<ScheduleDto>(schedule);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching schedule {scheduleId}");
            throw;
        }
    }

    public async Task<ScheduleDto> CreateScheduleAsync(CreateScheduleDto createScheduleDto)
    {
        try
        {
            _logger.LogInformation($"Creating new schedule for bus {createScheduleDto.BusId}");
            
            var schedule = new Schedule
            {
                BusId = createScheduleDto.BusId,
                RouteId = createScheduleDto.RouteId,
                DriverId = createScheduleDto.DriverId,
                HostessId = createScheduleDto.HostessId,
                DepartureTime = createScheduleDto.DepartureTime,
                ArrivalTime = createScheduleDto.ArrivalTime,
                ScheduleDate = createScheduleDto.ScheduleDate,
                Status = "Scheduled",
                IsActive = true
            };

            var createdSchedule = await _scheduleRepository.CreateAsync(schedule);
            _logger.LogInformation($"Schedule created successfully with ID: {createdSchedule.ScheduleId}");
            
            return _mapper.Map<ScheduleDto>(createdSchedule);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating schedule");
            throw;
        }
    }
}
