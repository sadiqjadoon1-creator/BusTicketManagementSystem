using BusTicketManagement.Application.DTOs.PaymentDTOs;
using BusTicketManagement.Application.Interfaces;
using BusTicketManagement.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
    {
        _paymentService = paymentService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<PaymentDto>>> ProcessPayment([FromBody] CreatePaymentDto createPaymentDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<PaymentDto>.FailureResponse("Invalid payment data"));

            var payment = await _paymentService.ProcessPaymentAsync(createPaymentDto);
            return Ok(ApiResponse<PaymentDto>.SuccessResponse(payment, "Payment processed successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing payment");
            return BadRequest(ApiResponse<PaymentDto>.FailureResponse(ex.Message));
        }
    }

    [HttpGet("booking/{bookingId}")]
    public async Task<ActionResult<ApiResponse<PaymentDto>>> GetPaymentByBookingId(int bookingId)
    {
        try
        {
            var payment = await _paymentService.GetPaymentByBookingIdAsync(bookingId);
            return Ok(ApiResponse<PaymentDto>.SuccessResponse(payment, "Payment retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving payment");
            return BadRequest(ApiResponse<PaymentDto>.FailureResponse(ex.Message));
        }
    }
}
