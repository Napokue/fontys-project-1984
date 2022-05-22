using DatabaseLib.Models;

namespace DatabaseLib.Factories;

public interface IConnectionFactory
{
    public IConnection Create(string connectionString);
}