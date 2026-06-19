namespace BusTicketManagement.Application.DTOs.BookingDTOs;

public class BookingDto
{
    public int BookingId { get; set; }
    public string BookingRef { get; set; } = string.Empty;
    public int ScheduleId { get; set; }
    public string PassengerId { get; set; } = string.Empty;
    public int TotalSeats { get; set; }
    public decimal TotalAmount { get; set; }
    public string BookingStatus { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; }
}

public class CreateBookingDto
{
    public int ScheduleId { get; set; }
    public int FromStopId { get; set; }
    public int ToStopId { get; set; }
    public List<int> SelectedSeatIds { get; set; } = new();
}

public class CancelBookingDto
{
    public int BookingId { get; set; }
    public string CancellationReason { get; set; } = string.Empty;
}
