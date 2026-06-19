using Microsoft.Data.SqlClient;
using BusTicketManagement.Infrastructure.Data;

namespace BusTicketManagement.Infrastructure.Repository;

public abstract class BaseRepository
{
    protected readonly IDbConnectionFactory _connectionFactory;

    protected BaseRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    protected async Task<T?> ExecuteScalarAsync<T>(string commandText, CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();
        using var command = new SqlCommand(commandText, connection) { CommandType = commandType };
        if (parameters.Length > 0)
            command.Parameters.AddRange(parameters);

        var result = await command.ExecuteScalarAsync();
        return result == null || result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
    }

    protected async Task<int> ExecuteNonQueryAsync(string commandText, CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();
        using var command = new SqlCommand(commandText, connection) { CommandType = commandType };
        if (parameters.Length > 0)
            command.Parameters.AddRange(parameters);

        return await command.ExecuteNonQueryAsync();
    }

    protected async Task<List<T>> ExecuteReaderAsync<T>(string commandText, Func<SqlDataReader, T> mapper, CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
    {
        var results = new List<T>();
        using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();
        using var command = new SqlCommand(commandText, connection) { CommandType = commandType };
        if (parameters.Length > 0)
            command.Parameters.AddRange(parameters);

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(mapper(reader));
        }

        return results;
    }
}
