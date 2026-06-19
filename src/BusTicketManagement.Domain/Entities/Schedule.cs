namespace BusTicketManagement.Domain.Entities;

public class Schedule
{
    public int ScheduleId { get; set; }
    public int BusId { get; set; }
    public int RouteId { get; set; }
    public int? DriverId { get; set; }
    public int? HostessId { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public DateTime ScheduleDate { get; set; }
    public string Status { get; set; } = "Scheduled";
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
