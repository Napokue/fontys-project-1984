using DatabaseLib;
using DatabaseLib.Builders;
using DatabaseLib.Factories;
using DatabaseLib.Models;
using DatabaseLib.Services;
using MessageService.Models;

namespace MessageService.Repository;

public class TextMessageRepository : IRepository<TextMessage>
{
    private const string ConnectionString =
        "Host=message-service-postgres:5432;Username=Admin;Password=P@ssWord1!;Database=message_service";
    private readonly IQueryService _queryService;
    private readonly IConnectionFactory _connectionFactory;
    private readonly IQueryCommandBuilderFactory<PostgresQueryCommandBuilder> _queryCommandBuilderFactory;
    
    public TextMessageRepository(IQueryService queryService, 
        IConnectionFactory connectionFactory,
        PostgresQueryCommandBuilderFactory queryCommandBuilderFactory)
    {
        _queryService = queryService;
        _connectionFactory = connectionFactory;
        _queryCommandBuilderFactory = queryCommandBuilderFactory;
    }

    public async Task<bool> AddAsync(TextMessage model)
    {
        var queryCommandBuilder = _queryCommandBuilderFactory.Create();

        await using var connection = (PostgresConnection) _connectionFactory.Create(ConnectionString);
        
        queryCommandBuilder
            .SetConnection(connection)
            .SetQuery(
                "INSERT INTO public.\"message\" (id, content) VALUES (DEFAULT, ($1)::text);")
            .AddParameter(model.Content);
        
        await using var command = queryCommandBuilder.Build();
        var queryResult = await _queryService.ExecuteNonQueryAsync(command);
        return queryResult == 1;
    }

    public async Task<TextMessage?> GetByIdAsync(Guid id)
    {
        var queryCommandBuilder = _queryCommandBuilderFactory.Create();

        await using var connection = (PostgresConnection) _connectionFactory.Create(ConnectionString);
        
        queryCommandBuilder
            .SetConnection(connection)
            .SetQuery(
                "SELECT * FROM public.\"message\" where id = ($1);")
            .AddParameter(id);
        
        await using var command = queryCommandBuilder.Build();
        var queryResult = await _queryService.ExecuteReaderAsync<IQueryCommand, TextMessage>(command);
        return queryResult.FirstOrDefault();
    }

    public async Task<List<TextMessage>> GetAllAsync(int skip, int take)
    {
        var queryCommandBuilder = _queryCommandBuilderFactory.Create();

        await using var connection = (PostgresConnection) _connectionFactory.Create(ConnectionString);
        
        queryCommandBuilder
            .SetConnection(connection)
            .SetQuery(
                "SELECT * FROM public.\"message\" LIMIT ($1) OFFSET ($2);")
            .AddParameter(take)
            .AddParameter(skip);
        
        await using var command = queryCommandBuilder.Build();
        var queryResult = await _queryService.ExecuteReaderAsync<IQueryCommand, TextMessage>(command);
        return queryResult.ToList();
    }

    public async Task<bool> UpdateAsync(TextMessage model)
    {
        var queryCommandBuilder = _queryCommandBuilderFactory.Create();

        await using var connection = (PostgresConnection) _connectionFactory.Create(ConnectionString);
        
        queryCommandBuilder
            .SetConnection(connection)
            .SetQuery(
                @"UPDATE public.""message""
                SET content = ($1)::text
                WHERE id = ($2)::uuid;")
            .AddParameter(model.Content)
            .AddParameter(model.Id);
        
        await using var command = queryCommandBuilder.Build();
        var queryResult = await _queryService.ExecuteNonQueryAsync(command);
        return queryResult == 1;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var queryCommandBuilder = new PostgresQueryCommandBuilder();

        await using var connection = (PostgresConnection) _connectionFactory.Create(ConnectionString);
        
        queryCommandBuilder
            .SetConnection(connection)
            .SetQuery(
                @"DELETE
                FROM public.""message""
                WHERE id = ($1)::uuid;")
            .AddParameter(id);
        
        await using var command = queryCommandBuilder.Build();
        var queryResult = await _queryService.ExecuteNonQueryAsync(command);
        return queryResult == 1;
    }
}