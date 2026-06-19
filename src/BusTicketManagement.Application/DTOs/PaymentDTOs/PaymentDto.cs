namespace BusTicketManagement.Application.DTOs.PaymentDTOs;

public class PaymentDto
{
    public int PaymentId { get; set; }
    public int BookingId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
    public DateTime? PaymentDate { get; set; }
}

public class CreatePaymentDto
{
    public int BookingId { get; set; }
    public int PaymentMethodId { get; set; }
    public decimal Amount { get; set; }
}
