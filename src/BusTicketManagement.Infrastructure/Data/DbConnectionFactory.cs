using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BusTicketManagement.Infrastructure.Data;

public interface IDbConnectionFactory
{
    SqlConnection CreateConnection();
}

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found");
    }

    public SqlConnection CreateConnection() => new(_connectionString);
}
