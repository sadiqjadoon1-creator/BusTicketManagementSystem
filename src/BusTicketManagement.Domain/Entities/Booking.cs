namespace BusTicketManagement.Domain.Entities;

public class Booking
{
    public int BookingId { get; set; }
    public string BookingRef { get; set; } = string.Empty;
    public int ScheduleId { get; set; }
    public string PassengerId { get; set; } = string.Empty;
    public int FromStopId { get; set; }
    public int ToStopId { get; set; }
    public int TotalSeats { get; set; }
    public decimal BaseFare { get; set; }
    public decimal Discount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string BookingStatus { get; set; } = "Pending";
    public string PaymentStatus { get; set; } = "Pending";
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    public DateTime? CancellationDate { get; set; }
    public string? CancellationReason { get; set; }
    public decimal RefundAmount { get; set; }
    public string? RefundStatus { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
