namespace BusTicketManagement.Application.DTOs.RouteDTOs;

public class RouteDto
{
    public int RouteId { get; set; }
    public string RouteName { get; set; } = string.Empty;
    public int SourceCityId { get; set; }
    public int DestinationCityId { get; set; }
    public decimal TotalDistance { get; set; }
    public int EstimatedDuration { get; set; }
    public decimal BaseFare { get; set; }
    public bool IsActive { get; set; }
}

public class CreateRouteDto
{
    public string RouteName { get; set; } = string.Empty;
    public int SourceCityId { get; set; }
    public int DestinationCityId { get; set; }
    public decimal TotalDistance { get; set; }
    public int EstimatedDuration { get; set; }
    public decimal BaseFare { get; set; }
}
