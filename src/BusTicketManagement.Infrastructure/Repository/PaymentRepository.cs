using BusTicketManagement.Domain.Entities;
using BusTicketManagement.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace BusTicketManagement.Infrastructure.Repository;

public interface IPaymentRepository
{
    Task<Payment> CreateAsync(Payment payment);
    Task<Payment?> GetByBookingIdAsync(int bookingId);
}

public class PaymentRepository : BaseRepository, IPaymentRepository
{
    public PaymentRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory) { }

    public async Task<Payment> CreateAsync(Payment payment)
    {
        const string sql = @"INSERT INTO Payments (BookingId, TransactionId, PaymentMethodId, Amount, PaymentStatus, PaymentDate, CreatedAt, UpdatedAt) 
                            VALUES (@BookingId, @TransactionId, @PaymentMethodId, @Amount, @PaymentStatus, @PaymentDate, @CreatedAt, @UpdatedAt);
                            SELECT SCOPE_IDENTITY();";
        
        var parameters = new[]
        {
            new SqlParameter("@BookingId", payment.BookingId),
            new SqlParameter("@TransactionId", payment.TransactionId),
            new SqlParameter("@PaymentMethodId", payment.PaymentMethodId),
            new SqlParameter("@Amount", payment.Amount),
            new SqlParameter("@PaymentStatus", payment.PaymentStatus),
            new SqlParameter("@PaymentDate", payment.PaymentDate ?? DBNull.Value),
            new SqlParameter("@CreatedAt", payment.CreatedAt),
            new SqlParameter("@UpdatedAt", payment.UpdatedAt)
        };

        var id = await ExecuteScalarAsync<int>(sql, CommandType.Text, parameters);
        payment.PaymentId = id;
        return payment;
    }

    public async Task<Payment?> GetByBookingIdAsync(int bookingId)
    {
        const string sql = "SELECT PaymentId, BookingId, TransactionId, PaymentMethodId, Amount, PaymentStatus, PaymentDate, CreatedAt, UpdatedAt FROM Payments WHERE BookingId = @BookingId ORDER BY CreatedAt DESC";
        var parameters = new[] { new SqlParameter("@BookingId", bookingId) };
        var result = await ExecuteReaderAsync(sql, MapPayment, CommandType.Text, parameters);
        return result.FirstOrDefault();
    }

    private Payment MapPayment(SqlDataReader reader) => new()
    {
        PaymentId = reader.GetInt32(0),
        BookingId = reader.GetInt32(1),
        TransactionId = reader.GetString(2),
        PaymentMethodId = reader.GetInt32(3),
        Amount = reader.GetDecimal(4),
        PaymentStatus = reader.GetString(5),
        PaymentDate = reader.IsDBNull(6) ? null : reader.GetDateTime(6),
        CreatedAt = reader.GetDateTime(7),
        UpdatedAt = reader.GetDateTime(8)
    };
}
