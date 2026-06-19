using BusTicketManagement.Domain.Entities;
using BusTicketManagement.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace BusTicketManagement.Infrastructure.Repository;

public interface IScheduleRepository
{
    Task<List<Schedule>> GetAllAsync();
    Task<Schedule?> GetByIdAsync(int id);
    Task<Schedule> CreateAsync(Schedule schedule);
    Task<List<Schedule>> GetByRouteAndDateAsync(int routeId, DateTime date);
}

public class ScheduleRepository : BaseRepository, IScheduleRepository
{
    public ScheduleRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory) { }

    public async Task<List<Schedule>> GetAllAsync()
    {
        const string sql = "SELECT ScheduleId, BusId, RouteId, DriverId, HostessId, DepartureTime, ArrivalTime, ScheduleDate, Status FROM Schedules WHERE IsActive = 1";
        return await ExecuteReaderAsync(sql, MapSchedule);
    }

    public async Task<Schedule?> GetByIdAsync(int id)
    {
        const string sql = "SELECT ScheduleId, BusId, RouteId, DriverId, HostessId, DepartureTime, ArrivalTime, ScheduleDate, Status FROM Schedules WHERE ScheduleId = @ScheduleId";
        var parameters = new[] { new SqlParameter("@ScheduleId", id) };
        var result = await ExecuteReaderAsync(sql, MapSchedule, CommandType.Text, parameters);
        return result.FirstOrDefault();
    }

    public async Task<Schedule> CreateAsync(Schedule schedule)
    {
        const string sql = @"INSERT INTO Schedules (BusId, RouteId, DriverId, HostessId, DepartureTime, ArrivalTime, ScheduleDate, Status, IsActive, CreatedAt, UpdatedAt) 
                            VALUES (@BusId, @RouteId, @DriverId, @HostessId, @DepartureTime, @ArrivalTime, @ScheduleDate, @Status, @IsActive, @CreatedAt, @UpdatedAt);
                            SELECT SCOPE_IDENTITY();";
        
        var parameters = new[]
        {
            new SqlParameter("@BusId", schedule.BusId),
            new SqlParameter("@RouteId", schedule.RouteId),
            new SqlParameter("@DriverId", schedule.DriverId ?? DBNull.Value),
            new SqlParameter("@HostessId", schedule.HostessId ?? DBNull.Value),
            new SqlParameter("@DepartureTime", schedule.DepartureTime),
            new SqlParameter("@ArrivalTime", schedule.ArrivalTime),
            new SqlParameter("@ScheduleDate", schedule.ScheduleDate),
            new SqlParameter("@Status", schedule.Status),
            new SqlParameter("@IsActive", schedule.IsActive),
            new SqlParameter("@CreatedAt", schedule.CreatedAt),
            new SqlParameter("@UpdatedAt", schedule.UpdatedAt)
        };

        var id = await ExecuteScalarAsync<int>(sql, CommandType.Text, parameters);
        schedule.ScheduleId = id;
        return schedule;
    }

    public async Task<List<Schedule>> GetByRouteAndDateAsync(int routeId, DateTime date)
    {
        const string sql = @"SELECT ScheduleId, BusId, RouteId, DriverId, HostessId, DepartureTime, ArrivalTime, ScheduleDate, Status 
                            FROM Schedules WHERE RouteId = @RouteId AND CAST(ScheduleDate AS DATE) = @ScheduleDate AND IsActive = 1";
        var parameters = new[]
        {
            new SqlParameter("@RouteId", routeId),
            new SqlParameter("@ScheduleDate", date.Date)
        };
        return await ExecuteReaderAsync(sql, MapSchedule, CommandType.Text, parameters);
    }

    private Schedule MapSchedule(SqlDataReader reader) => new()
    {
        ScheduleId = reader.GetInt32(0),
        BusId = reader.GetInt32(1),
        RouteId = reader.GetInt32(2),
        DriverId = reader.IsDBNull(3) ? null : reader.GetInt32(3),
        HostessId = reader.IsDBNull(4) ? null : reader.GetInt32(4),
        DepartureTime = reader.GetDateTime(5),
        ArrivalTime = reader.GetDateTime(6),
        ScheduleDate = reader.GetDateTime(7),
        Status = reader.GetString(8)
    };
}
