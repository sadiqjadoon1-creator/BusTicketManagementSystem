using BusTicketManagement.Application.DTOs.BookingDTOs;

namespace BusTicketManagement.Application.Interfaces;

public interface IBookingService
{
    Task<BookingDto> CreateBookingAsync(CreateBookingDto createBookingDto, string userId);
    Task<List<BookingDto>> GetUserBookingsAsync(string userId);
    Task<BookingDto> GetBookingByIdAsync(int bookingId);
    Task<bool> CancelBookingAsync(CancelBookingDto cancelBookingDto);
}
