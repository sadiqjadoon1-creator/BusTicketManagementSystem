using BusTicketManagement.Domain.Entities;
using BusTicketManagement.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace BusTicketManagement.Infrastructure.Repository;

public interface IBusRepository
{
    Task<List<Bus>> GetAllAsync();
    Task<Bus?> GetByIdAsync(int id);
    Task<Bus> CreateAsync(Bus bus);
    Task<Bus> UpdateAsync(Bus bus);
    Task<bool> DeleteAsync(int id);
}

public class BusRepository : BaseRepository, IBusRepository
{
    public BusRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory) { }

    public async Task<List<Bus>> GetAllAsync()
    {
        const string sql = "SELECT BusId, BusNo, BusTypeId, TotalCapacity, CurrentOccupancy, RegistrationNumber, ManufacturerModel, YearOfManufacture, IsActive, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy FROM Buses WHERE IsActive = 1";
        return await ExecuteReaderAsync(sql, MapBus);
    }

    public async Task<Bus?> GetByIdAsync(int id)
    {
        const string sql = "SELECT BusId, BusNo, BusTypeId, TotalCapacity, CurrentOccupancy, RegistrationNumber, ManufacturerModel, YearOfManufacture, IsActive, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy FROM Buses WHERE BusId = @BusId";
        var parameters = new[] { new SqlParameter("@BusId", id) };
        var result = await ExecuteReaderAsync(sql, MapBus, CommandType.Text, parameters);
        return result.FirstOrDefault();
    }

    public async Task<Bus> CreateAsync(Bus bus)
    {
        const string sql = @"INSERT INTO Buses (BusNo, BusTypeId, TotalCapacity, CurrentOccupancy, RegistrationNumber, ManufacturerModel, YearOfManufacture, PurchaseDate, IsActive, CreatedAt, UpdatedAt, CreatedBy) 
                            VALUES (@BusNo, @BusTypeId, @TotalCapacity, @CurrentOccupancy, @RegistrationNumber, @ManufacturerModel, @YearOfManufacture, @PurchaseDate, @IsActive, @CreatedAt, @UpdatedAt, @CreatedBy);
                            SELECT SCOPE_IDENTITY();";
        
        var parameters = new[]
        {
            new SqlParameter("@BusNo", bus.BusNo),
            new SqlParameter("@BusTypeId", bus.BusTypeId),
            new SqlParameter("@TotalCapacity", bus.TotalCapacity),
            new SqlParameter("@CurrentOccupancy", bus.CurrentOccupancy),
            new SqlParameter("@RegistrationNumber", bus.RegistrationNumber),
            new SqlParameter("@ManufacturerModel", bus.ManufacturerModel),
            new SqlParameter("@YearOfManufacture", bus.YearOfManufacture),
            new SqlParameter("@PurchaseDate", bus.PurchaseDate),
            new SqlParameter("@IsActive", bus.IsActive),
            new SqlParameter("@CreatedAt", bus.CreatedAt),
            new SqlParameter("@UpdatedAt", bus.UpdatedAt),
            new SqlParameter("@CreatedBy", bus.CreatedBy ?? DBNull.Value)
        };

        var id = await ExecuteScalarAsync<int>(sql, CommandType.Text, parameters);
        bus.BusId = id;
        return bus;
    }

    public async Task<Bus> UpdateAsync(Bus bus)
    {
        const string sql = @"UPDATE Buses SET BusNo = @BusNo, BusTypeId = @BusTypeId, TotalCapacity = @TotalCapacity, CurrentOccupancy = @CurrentOccupancy, 
                            RegistrationNumber = @RegistrationNumber, ManufacturerModel = @ManufacturerModel, YearOfManufacture = @YearOfManufacture, 
                            UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy WHERE BusId = @BusId";
        
        var parameters = new[]
        {
            new SqlParameter("@BusId", bus.BusId),
            new SqlParameter("@BusNo", bus.BusNo),
            new SqlParameter("@BusTypeId", bus.BusTypeId),
            new SqlParameter("@TotalCapacity", bus.TotalCapacity),
            new SqlParameter("@CurrentOccupancy", bus.CurrentOccupancy),
            new SqlParameter("@RegistrationNumber", bus.RegistrationNumber),
            new SqlParameter("@ManufacturerModel", bus.ManufacturerModel),
            new SqlParameter("@YearOfManufacture", bus.YearOfManufacture),
            new SqlParameter("@UpdatedAt", bus.UpdatedAt),
            new SqlParameter("@UpdatedBy", bus.UpdatedBy ?? DBNull.Value)
        };

        await ExecuteNonQueryAsync(sql, CommandType.Text, parameters);
        return bus;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        const string sql = "UPDATE Buses SET IsActive = 0 WHERE BusId = @BusId";
        var parameters = new[] { new SqlParameter("@BusId", id) };
        var result = await ExecuteNonQueryAsync(sql, CommandType.Text, parameters);
        return result > 0;
    }

    private Bus MapBus(SqlDataReader reader) => new()
    {
        BusId = reader.GetInt32(0),
        BusNo = reader.GetString(1),
        BusTypeId = reader.GetInt32(2),
        TotalCapacity = reader.GetInt32(3),
        CurrentOccupancy = reader.GetInt32(4),
        RegistrationNumber = reader.GetString(5),
        ManufacturerModel = reader.GetString(6),
        YearOfManufacture = reader.GetInt32(7),
        IsActive = reader.GetBoolean(8),
        CreatedAt = reader.GetDateTime(9),
        UpdatedAt = reader.GetDateTime(10),
        CreatedBy = reader.IsDBNull(11) ? null : reader.GetString(11),
        UpdatedBy = reader.IsDBNull(12) ? null : reader.GetString(12)
    };
}
