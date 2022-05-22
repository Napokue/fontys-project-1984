using DatabaseLib.Models;

namespace DatabaseLib.Factories;

public class PostgresConnectionFactory : IConnectionFactory
{
    public IConnection Create(string connectionString)
    {
        return new PostgresConnection(connectionString);
    }
}