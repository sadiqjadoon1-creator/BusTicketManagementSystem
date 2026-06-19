namespace BusTicketManagement.Application.DTOs.ScheduleDTOs;

public class ScheduleDto
{
    public int ScheduleId { get; set; }
    public int BusId { get; set; }
    public int RouteId { get; set; }
    public int? DriverId { get; set; }
    public int? HostessId { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public DateTime ScheduleDate { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class CreateScheduleDto
{
    public int BusId { get; set; }
    public int RouteId { get; set; }
    public int? DriverId { get; set; }
    public int? HostessId { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public DateTime ScheduleDate { get; set; }
}
