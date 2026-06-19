using BusTicketManagement.Application.DTOs.PaymentDTOs;

namespace BusTicketManagement.Application.Interfaces;

public interface IPaymentService
{
    Task<PaymentDto> ProcessPaymentAsync(CreatePaymentDto createPaymentDto);
    Task<PaymentDto> GetPaymentByBookingIdAsync(int bookingId);
}
