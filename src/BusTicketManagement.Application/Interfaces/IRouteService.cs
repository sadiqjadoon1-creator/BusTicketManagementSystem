using BusTicketManagement.Application.DTOs.RouteDTOs;

namespace BusTicketManagement.Application.Interfaces;

public interface IRouteService
{
    Task<List<RouteDto>> GetAllRoutesAsync();
    Task<RouteDto> GetRouteByIdAsync(int routeId);
    Task<RouteDto> CreateRouteAsync(CreateRouteDto createRouteDto);
    Task<List<RouteDto>> GetRoutesByCitiesAsync(int sourceCityId, int destinationCityId);
}
