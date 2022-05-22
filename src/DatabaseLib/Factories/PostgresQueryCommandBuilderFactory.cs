using DatabaseLib.Builders;

namespace DatabaseLib.Factories;

public class PostgresQueryCommandBuilderFactory : IQueryCommandBuilderFactory<PostgresQueryCommandBuilder>
{
    public PostgresQueryCommandBuilder Create()
    {
        return new PostgresQueryCommandBuilder();
    }
}