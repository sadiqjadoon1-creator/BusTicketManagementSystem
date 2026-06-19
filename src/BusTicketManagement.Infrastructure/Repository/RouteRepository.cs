using BusTicketManagement.Domain.Entities;
using BusTicketManagement.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace BusTicketManagement.Infrastructure.Repository;

public interface IRouteRepository
{
    Task<List<Route>> GetAllAsync();
    Task<Route?> GetByIdAsync(int id);
    Task<Route> CreateAsync(Route route);
    Task<List<Route>> GetRoutesByCitiesAsync(int sourceCityId, int destinationCityId);
}

public class RouteRepository : BaseRepository, IRouteRepository
{
    public RouteRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory) { }

    public async Task<List<Route>> GetAllAsync()
    {
        const string sql = "SELECT RouteId, RouteName, SourceCityId, DestinationCityId, TotalDistance, EstimatedDuration, BaseFare, IsActive FROM Routes WHERE IsActive = 1";
        return await ExecuteReaderAsync(sql, MapRoute);
    }

    public async Task<Route?> GetByIdAsync(int id)
    {
        const string sql = "SELECT RouteId, RouteName, SourceCityId, DestinationCityId, TotalDistance, EstimatedDuration, BaseFare, IsActive FROM Routes WHERE RouteId = @RouteId";
        var parameters = new[] { new SqlParameter("@RouteId", id) };
        var result = await ExecuteReaderAsync(sql, MapRoute, CommandType.Text, parameters);
        return result.FirstOrDefault();
    }

    public async Task<Route> CreateAsync(Route route)
    {
        const string sql = @"INSERT INTO Routes (RouteName, SourceCityId, DestinationCityId, TotalDistance, EstimatedDuration, BaseFare, IsActive, CreatedAt, UpdatedAt) 
                            VALUES (@RouteName, @SourceCityId, @DestinationCityId, @TotalDistance, @EstimatedDuration, @BaseFare, @IsActive, @CreatedAt, @UpdatedAt);
                            SELECT SCOPE_IDENTITY();";
        
        var parameters = new[]
        {
            new SqlParameter("@RouteName", route.RouteName),
            new SqlParameter("@SourceCityId", route.SourceCityId),
            new SqlParameter("@DestinationCityId", route.DestinationCityId),
            new SqlParameter("@TotalDistance", route.TotalDistance),
            new SqlParameter("@EstimatedDuration", route.EstimatedDuration),
            new SqlParameter("@BaseFare", route.BaseFare),
            new SqlParameter("@IsActive", route.IsActive),
            new SqlParameter("@CreatedAt", route.CreatedAt),
            new SqlParameter("@UpdatedAt", route.UpdatedAt)
        };

        var id = await ExecuteScalarAsync<int>(sql, CommandType.Text, parameters);
        route.RouteId = id;
        return route;
    }

    public async Task<List<Route>> GetRoutesByCitiesAsync(int sourceCityId, int destinationCityId)
    {
        const string sql = @"SELECT RouteId, RouteName, SourceCityId, DestinationCityId, TotalDistance, EstimatedDuration, BaseFare, IsActive 
                            FROM Routes WHERE SourceCityId = @SourceCityId AND DestinationCityId = @DestinationCityId AND IsActive = 1";
        var parameters = new[]
        {
            new SqlParameter("@SourceCityId", sourceCityId),
            new SqlParameter("@DestinationCityId", destinationCityId)
        };
        return await ExecuteReaderAsync(sql, MapRoute, CommandType.Text, parameters);
    }

    private Route MapRoute(SqlDataReader reader) => new()
    {
        RouteId = reader.GetInt32(0),
        RouteName = reader.GetString(1),
        SourceCityId = reader.GetInt32(2),
        DestinationCityId = reader.GetInt32(3),
        TotalDistance = reader.GetDecimal(4),
        EstimatedDuration = reader.GetInt32(5),
        BaseFare = reader.GetDecimal(6),
        IsActive = reader.GetBoolean(7)
    };
}
