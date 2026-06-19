using BusTicketManagement.Application.DTOs.PaymentDTOs;
using BusTicketManagement.Application.Interfaces;
using BusTicketManagement.Domain.Entities;
using BusTicketManagement.Infrastructure.Repository;
using AutoMapper;

namespace BusTicketManagement.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(IPaymentRepository paymentRepository, IMapper mapper, ILogger<PaymentService> logger)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PaymentDto> ProcessPaymentAsync(CreatePaymentDto createPaymentDto)
    {
        try
        {
            _logger.LogInformation($"Processing payment for booking {createPaymentDto.BookingId}");
            
            var payment = new Payment
            {
                BookingId = createPaymentDto.BookingId,
                PaymentMethodId = createPaymentDto.PaymentMethodId,
                Amount = createPaymentDto.Amount,
                TransactionId = $"TXN{DateTime.UtcNow.Ticks}",
                PaymentStatus = "Pending"
            };

            // Mock payment processing
            var random = new Random();
            payment.PaymentStatus = random.Next(0, 2) == 0 ? "Paid" : "Pending";
            payment.PaymentDate = payment.PaymentStatus == "Paid" ? DateTime.UtcNow : null;

            var createdPayment = await _paymentRepository.CreateAsync(payment);
            _logger.LogInformation($"Payment processed with ID: {createdPayment.PaymentId}, Status: {createdPayment.PaymentStatus}");
            
            return _mapper.Map<PaymentDto>(createdPayment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing payment");
            throw;
        }
    }

    public async Task<PaymentDto> GetPaymentByBookingIdAsync(int bookingId)
    {
        try
        {
            _logger.LogInformation($"Fetching payment for booking {bookingId}");
            var payment = await _paymentRepository.GetByBookingIdAsync(bookingId);
            if (payment == null)
                throw new InvalidOperationException($"Payment for booking {bookingId} not found");
            
            return _mapper.Map<PaymentDto>(payment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching payment");
            throw;
        }
    }
}
