using BusTicketManagement.Domain.Entities;
using BusTicketManagement.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace BusTicketManagement.Infrastructure.Repository;

public interface IBookingRepository
{
    Task<List<Booking>> GetAllAsync();
    Task<Booking?> GetByIdAsync(int id);
    Task<Booking> CreateAsync(Booking booking);
    Task<Booking> UpdateAsync(Booking booking);
    Task<List<Booking>> GetByUserIdAsync(string userId);
}

public class BookingRepository : BaseRepository, IBookingRepository
{
    public BookingRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory) { }

    public async Task<List<Booking>> GetAllAsync()
    {
        const string sql = "SELECT BookingId, BookingRef, ScheduleId, PassengerId, FromStopId, ToStopId, TotalSeats, BaseFare, Discount, TaxAmount, TotalAmount, BookingStatus, PaymentStatus, BookingDate FROM Bookings WHERE IsActive = 1";
        return await ExecuteReaderAsync(sql, MapBooking);
    }

    public async Task<Booking?> GetByIdAsync(int id)
    {
        const string sql = "SELECT BookingId, BookingRef, ScheduleId, PassengerId, FromStopId, ToStopId, TotalSeats, BaseFare, Discount, TaxAmount, TotalAmount, BookingStatus, PaymentStatus, BookingDate FROM Bookings WHERE BookingId = @BookingId";
        var parameters = new[] { new SqlParameter("@BookingId", id) };
        var result = await ExecuteReaderAsync(sql, MapBooking, CommandType.Text, parameters);
        return result.FirstOrDefault();
    }

    public async Task<Booking> CreateAsync(Booking booking)
    {
        const string sql = @"INSERT INTO Bookings (BookingRef, ScheduleId, PassengerId, FromStopId, ToStopId, TotalSeats, BaseFare, Discount, TaxAmount, TotalAmount, BookingStatus, PaymentStatus, BookingDate, IsActive, CreatedAt, UpdatedAt) 
                            VALUES (@BookingRef, @ScheduleId, @PassengerId, @FromStopId, @ToStopId, @TotalSeats, @BaseFare, @Discount, @TaxAmount, @TotalAmount, @BookingStatus, @PaymentStatus, @BookingDate, @IsActive, @CreatedAt, @UpdatedAt);
                            SELECT SCOPE_IDENTITY();";
        
        var parameters = new[]
        {
            new SqlParameter("@BookingRef", booking.BookingRef),
            new SqlParameter("@ScheduleId", booking.ScheduleId),
            new SqlParameter("@PassengerId", booking.PassengerId),
            new SqlParameter("@FromStopId", booking.FromStopId),
            new SqlParameter("@ToStopId", booking.ToStopId),
            new SqlParameter("@TotalSeats", booking.TotalSeats),
            new SqlParameter("@BaseFare", booking.BaseFare),
            new SqlParameter("@Discount", booking.Discount),
            new SqlParameter("@TaxAmount", booking.TaxAmount),
            new SqlParameter("@TotalAmount", booking.TotalAmount),
            new SqlParameter("@BookingStatus", booking.BookingStatus),
            new SqlParameter("@PaymentStatus", booking.PaymentStatus),
            new SqlParameter("@BookingDate", booking.BookingDate),
            new SqlParameter("@IsActive", booking.IsActive),
            new SqlParameter("@CreatedAt", booking.CreatedAt),
            new SqlParameter("@UpdatedAt", booking.UpdatedAt)
        };

        var id = await ExecuteScalarAsync<int>(sql, CommandType.Text, parameters);
        booking.BookingId = id;
        return booking;
    }

    public async Task<Booking> UpdateAsync(Booking booking)
    {
        const string sql = @"UPDATE Bookings SET BookingStatus = @BookingStatus, PaymentStatus = @PaymentStatus, CancellationDate = @CancellationDate, 
                            CancellationReason = @CancellationReason, RefundAmount = @RefundAmount, RefundStatus = @RefundStatus, UpdatedAt = @UpdatedAt 
                            WHERE BookingId = @BookingId";
        
        var parameters = new[]
        {
            new SqlParameter("@BookingId", booking.BookingId),
            new SqlParameter("@BookingStatus", booking.BookingStatus),
            new SqlParameter("@PaymentStatus", booking.PaymentStatus),
            new SqlParameter("@CancellationDate", booking.CancellationDate ?? DBNull.Value),
            new SqlParameter("@CancellationReason", booking.CancellationReason ?? DBNull.Value),
            new SqlParameter("@RefundAmount", booking.RefundAmount),
            new SqlParameter("@RefundStatus", booking.RefundStatus ?? DBNull.Value),
            new SqlParameter("@UpdatedAt", booking.UpdatedAt)
        };

        await ExecuteNonQueryAsync(sql, CommandType.Text, parameters);
        return booking;
    }

    public async Task<List<Booking>> GetByUserIdAsync(string userId)
    {
        const string sql = "SELECT BookingId, BookingRef, ScheduleId, PassengerId, FromStopId, ToStopId, TotalSeats, BaseFare, Discount, TaxAmount, TotalAmount, BookingStatus, PaymentStatus, BookingDate FROM Bookings WHERE PassengerId = @PassengerId AND IsActive = 1";
        var parameters = new[] { new SqlParameter("@PassengerId", userId) };
        return await ExecuteReaderAsync(sql, MapBooking, CommandType.Text, parameters);
    }

    private Booking MapBooking(SqlDataReader reader) => new()
    {
        BookingId = reader.GetInt32(0),
        BookingRef = reader.GetString(1),
        ScheduleId = reader.GetInt32(2),
        PassengerId = reader.GetString(3),
        FromStopId = reader.GetInt32(4),
        ToStopId = reader.GetInt32(5),
        TotalSeats = reader.GetInt32(6),
        BaseFare = reader.GetDecimal(7),
        Discount = reader.GetDecimal(8),
        TaxAmount = reader.GetDecimal(9),
        TotalAmount = reader.GetDecimal(10),
        BookingStatus = reader.GetString(11),
        PaymentStatus = reader.GetString(12),
        BookingDate = reader.GetDateTime(13)
    };
}
