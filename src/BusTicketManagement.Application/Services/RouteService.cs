using BusTicketManagement.Application.DTOs.RouteDTOs;
using BusTicketManagement.Application.Interfaces;
using BusTicketManagement.Domain.Entities;
using BusTicketManagement.Infrastructure.Repository;
using AutoMapper;

namespace BusTicketManagement.Application.Services;

public class RouteService : IRouteService
{
    private readonly IRouteRepository _routeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<RouteService> _logger;

    public RouteService(IRouteRepository routeRepository, IMapper mapper, ILogger<RouteService> logger)
    {
        _routeRepository = routeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<RouteDto>> GetAllRoutesAsync()
    {
        try
        {
            _logger.LogInformation("Fetching all routes");
            var routes = await _routeRepository.GetAllAsync();
            return _mapper.Map<List<RouteDto>>(routes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching routes");
            throw;
        }
    }

    public async Task<RouteDto> GetRouteByIdAsync(int routeId)
    {
        try
        {
            _logger.LogInformation($"Fetching route with ID: {routeId}");
            var route = await _routeRepository.GetByIdAsync(routeId);
            if (route == null)
                throw new InvalidOperationException($"Route with ID {routeId} not found");
            
            return _mapper.Map<RouteDto>(route);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching route {routeId}");
            throw;
        }
    }

    public async Task<RouteDto> CreateRouteAsync(CreateRouteDto createRouteDto)
    {
        try
        {
            _logger.LogInformation($"Creating new route: {createRouteDto.RouteName}");
            
            var route = new Route
            {
                RouteName = createRouteDto.RouteName,
                SourceCityId = createRouteDto.SourceCityId,
                DestinationCityId = createRouteDto.DestinationCityId,
                TotalDistance = createRouteDto.TotalDistance,
                EstimatedDuration = createRouteDto.EstimatedDuration,
                BaseFare = createRouteDto.BaseFare,
                IsActive = true
            };

            var createdRoute = await _routeRepository.CreateAsync(route);
            _logger.LogInformation($"Route created successfully with ID: {createdRoute.RouteId}");
            
            return _mapper.Map<RouteDto>(createdRoute);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating route");
            throw;
        }
    }

    public async Task<List<RouteDto>> GetRoutesByCitiesAsync(int sourceCityId, int destinationCityId)
    {
        try
        {
            _logger.LogInformation($"Fetching routes from city {sourceCityId} to {destinationCityId}");
            var routes = await _routeRepository.GetRoutesByCitiesAsync(sourceCityId, destinationCityId);
            return _mapper.Map<List<RouteDto>>(routes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching routes by cities");
            throw;
        }
    }
}
