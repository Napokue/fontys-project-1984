using System.Data;
using DatabaseLib.Models;
using Npgsql;

namespace DatabaseLib.Builders;

public class PostgresQueryCommandBuilder : IQueryCommandBuilder
{
    private string? _query;
    private readonly List<object> _values;
    private PostgresConnection? _connection;

    public PostgresQueryCommandBuilder()
    {
        _values = new List<object>(0);
    }

    public PostgresQueryCommandBuilder SetQuery(string query)
    {
        _query = query;
        return this;
    }

    public PostgresQueryCommandBuilder SetConnection(PostgresConnection connection)
    {
        _connection = connection;
        return this;
    }

    public PostgresQueryCommandBuilder AddParameter(object value)
    {
        _values.Add(value);
        return this;
    }

    public PostgresQueryCommandBuilder AddParameters(IEnumerable<string> values)
    {
        foreach (var value in values)
        {
            AddParameter(value);
        }

        return this;
    }
    
    public IQueryCommand Build()
    {
        var command = new NpgsqlCommand(_query, _connection!);

        foreach (var value in _values)
        {
            command.Parameters.Add(new NpgsqlParameter
            {
                Value = value,
                DbType = GetDbType(value)
            });
        }
        command.Prepare();
        return new PostgresQueryCommand(command);
    }

    private DbType GetDbType(object obj) =>
        obj switch
        {
            string => DbType.String,
            Guid => DbType.Guid,
            int => DbType.Int32,
            _ => throw new ArgumentNullException(nameof(obj), $"Type: {obj.GetType()} not supported.")
        };
}