using Npgsql;

namespace DatabaseLib.Models;

public class PostgresConnection : IConnection
{
    private readonly NpgsqlConnection _connection;

    public static implicit operator NpgsqlConnection(PostgresConnection connection) => connection._connection;
    
    public PostgresConnection(string connectionString)
    {
        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
    }
    
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);
        Dispose(false);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }
        
        _connection.Close();
        _connection.Dispose();
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        await _connection.DisposeAsync().ConfigureAwait(false);
    }
}