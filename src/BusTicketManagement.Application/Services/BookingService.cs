using BusTicketManagement.Application.DTOs.BookingDTOs;
using BusTicketManagement.Application.Interfaces;
using BusTicketManagement.Domain.Entities;
using BusTicketManagement.Infrastructure.Repository;
using AutoMapper;

namespace BusTicketManagement.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<BookingService> _logger;

    public BookingService(IBookingRepository bookingRepository, IMapper mapper, ILogger<BookingService> logger)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BookingDto> CreateBookingAsync(CreateBookingDto createBookingDto, string userId)
    {
        try
        {
            _logger.LogInformation($"Creating booking for user: {userId}, Schedule: {createBookingDto.ScheduleId}");
            
            var bookingRef = $"BK{DateTime.UtcNow.Ticks}";
            var booking = new Booking
            {
                BookingRef = bookingRef,
                ScheduleId = createBookingDto.ScheduleId,
                PassengerId = userId,
                FromStopId = createBookingDto.FromStopId,
                ToStopId = createBookingDto.ToStopId,
                TotalSeats = createBookingDto.SelectedSeatIds.Count,
                BookingStatus = "Pending",
                PaymentStatus = "Pending",
                IsActive = true
            };

            var createdBooking = await _bookingRepository.CreateAsync(booking);
            _logger.LogInformation($"Booking created successfully with ID: {createdBooking.BookingId}");
            
            return _mapper.Map<BookingDto>(createdBooking);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating booking");
            throw;
        }
    }

    public async Task<List<BookingDto>> GetUserBookingsAsync(string userId)
    {
        try
        {
            _logger.LogInformation($"Fetching bookings for user: {userId}");
            var bookings = await _bookingRepository.GetByUserIdAsync(userId);
            return _mapper.Map<List<BookingDto>>(bookings);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching user bookings");
            throw;
        }
    }

    public async Task<BookingDto> GetBookingByIdAsync(int bookingId)
    {
        try
        {
            _logger.LogInformation($"Fetching booking with ID: {bookingId}");
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
                throw new InvalidOperationException($"Booking with ID {bookingId} not found");
            
            return _mapper.Map<BookingDto>(booking);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching booking {bookingId}");
            throw;
        }
    }

    public async Task<bool> CancelBookingAsync(CancelBookingDto cancelBookingDto)
    {
        try
        {
            _logger.LogInformation($"Cancelling booking with ID: {cancelBookingDto.BookingId}");
            
            var booking = await _bookingRepository.GetByIdAsync(cancelBookingDto.BookingId);
            if (booking == null)
                throw new InvalidOperationException($"Booking with ID {cancelBookingDto.BookingId} not found");

            var hoursUntilDeparture = 48; // Mock value
            var canCancel = hoursUntilDeparture > 48;
            var refundPercentage = canCancel ? 100 : 70; // 30% deduction if within 48 hours

            booking.BookingStatus = "Cancelled";
            booking.CancellationDate = DateTime.UtcNow;
            booking.CancellationReason = cancelBookingDto.CancellationReason;
            booking.RefundAmount = booking.TotalAmount * (refundPercentage / 100m);
            booking.RefundStatus = "Pending";

            await _bookingRepository.UpdateAsync(booking);
            _logger.LogInformation($"Booking cancelled successfully with ID: {cancelBookingDto.BookingId}");
            
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cancelling booking");
            throw;
        }
    }
}
