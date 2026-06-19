using BusTicketManagement.Application.DTOs.BookingDTOs;
using BusTicketManagement.Application.Interfaces;
using BusTicketManagement.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;
    private readonly ILogger<BookingController> _logger;

    public BookingController(IBookingService bookingService, ILogger<BookingController> logger)
    {
        _bookingService = bookingService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<BookingDTOs.BookingDto>>> CreateBooking([FromBody] CreateBookingDto createBookingDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<BookingDTOs.BookingDto>.FailureResponse("Invalid booking data"));

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(ApiResponse<BookingDTOs.BookingDto>.FailureResponse("User not authenticated"));

            var booking = await _bookingService.CreateBookingAsync(createBookingDto, userId);
            return CreatedAtAction(nameof(GetBooking), new { id = booking.BookingId }, ApiResponse<BookingDTOs.BookingDto>.SuccessResponse(booking, "Booking created successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating booking");
            return BadRequest(ApiResponse<BookingDTOs.BookingDto>.FailureResponse(ex.Message));
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<ApiResponse<List<BookingDTOs.BookingDto>>>> GetUserBookings(string userId)
    {
        try
        {
            var bookings = await _bookingService.GetUserBookingsAsync(userId);
            return Ok(ApiResponse<List<BookingDTOs.BookingDto>>.SuccessResponse(bookings, "User bookings retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user bookings");
            return BadRequest(ApiResponse<List<BookingDTOs.BookingDto>>.FailureResponse(ex.Message));
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<BookingDTOs.BookingDto>>> GetBooking(int id)
    {
        try
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            return Ok(ApiResponse<BookingDTOs.BookingDto>.SuccessResponse(booking, "Booking retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving booking");
            return BadRequest(ApiResponse<BookingDTOs.BookingDto>.FailureResponse(ex.Message));
        }
    }

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult<ApiResponse<bool>>> CancelBooking(int id, [FromBody] CancelBookingDto cancelBookingDto)
    {
        try
        {
            cancelBookingDto.BookingId = id;
            var result = await _bookingService.CancelBookingAsync(cancelBookingDto);
            return Ok(ApiResponse<bool>.SuccessResponse(result, "Booking cancelled successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cancelling booking");
            return BadRequest(ApiResponse<bool>.FailureResponse(ex.Message));
        }
    }
}
