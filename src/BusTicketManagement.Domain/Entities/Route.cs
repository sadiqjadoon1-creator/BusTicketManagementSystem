namespace BusTicketManagement.Domain.Entities;

public class Route
{
    public int RouteId { get; set; }
    public string RouteName { get; set; } = string.Empty;
    public int SourceCityId { get; set; }
    public int DestinationCityId { get; set; }
    public decimal TotalDistance { get; set; }
    public int EstimatedDuration { get; set; }
    public decimal BaseFare { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
