using BusTicketManagement.Application.DTOs.ScheduleDTOs;

namespace BusTicketManagement.Application.Interfaces;

public interface IScheduleService
{
    Task<List<ScheduleDto>> GetSchedulesByRouteAsync(int routeId, DateTime date);
    Task<ScheduleDto> GetScheduleByIdAsync(int scheduleId);
    Task<ScheduleDto> CreateScheduleAsync(CreateScheduleDto createScheduleDto);
}
