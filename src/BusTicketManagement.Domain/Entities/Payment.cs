namespace BusTicketManagement.Domain.Entities;

public class Payment
{
    public int PaymentId { get; set; }
    public int BookingId { get; set; }
    public string TransactionId { get; set; } = string.Empty;
    public int PaymentMethodId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentStatus { get; set; } = "Pending";
    public DateTime? PaymentDate { get; set; }
    public DateTime? RefundDate { get; set; }
    public decimal? RefundAmount { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
