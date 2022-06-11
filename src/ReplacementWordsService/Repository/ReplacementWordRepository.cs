using DatabaseLib;
using DatabaseLib.Builders;
using DatabaseLib.Factories;
using DatabaseLib.Models;
using DatabaseLib.Services;
using ReplacementWordsService.Models;

namespace ReplacementWordsService.Repository;

public class ReplacementWordRepository : IRepository<ReplacementWord>
{
    private const string ConnectionString =
        "Host=replacement-words-service-postgres:5432;Username=Admin;Password=P@ssWord1!;Database=replacement_words_service";
    
    private readonly IQueryService _queryService;
    private readonly IConnectionFactory _connectionFactory;
    private readonly IQueryCommandBuilderFactory<PostgresQueryCommandBuilder> _queryCommandBuilderFactory;

    public ReplacementWordRepository(
        IQueryService queryService, 
        IConnectionFactory connectionFactory, 
        IQueryCommandBuilderFactory<PostgresQueryCommandBuilder> queryCommandBuilderFactory)
    {
        _queryService = queryService;
        _connectionFactory = connectionFactory;
        _queryCommandBuilderFactory = queryCommandBuilderFactory;
    }

    public async Task<bool> AddAsync(ReplacementWord model)
    {
        var queryCommandBuilder = _queryCommandBuilderFactory.Create();

        await using var connection = (PostgresConnection) _connectionFactory.Create(ConnectionString);

        queryCommandBuilder
            .SetConnection(connection)
            .SetQuery(
                "INSERT INTO public.\"replacement_words\" (id, oldspeak, newspeak) VALUES (DEFAULT, ($1)::text, ($2)::text);")
            .AddParameter(model.Oldspeak)
            .AddParameter(model.Newspeak);
        
        await using var command = queryCommandBuilder.Build();
        var queryResult = await _queryService.ExecuteNonQueryAsync(command);
        return queryResult == 1;
    }

    public async Task<ReplacementWord?> GetByIdAsync(Guid id)
    {
        var queryCommandBuilder = _queryCommandBuilderFactory.Create();

        await using var connection = (PostgresConnection) _connectionFactory.Create(ConnectionString);
        
        queryCommandBuilder
            .SetConnection(connection)
            .SetQuery(
                "SELECT * FROM public.\"replacement_words\" where id = ($1);")
            .AddParameter(id);
        
        await using var command = queryCommandBuilder.Build();
        var queryResult = await _queryService.ExecuteReaderAsync<IQueryCommand, ReplacementWord>(command);
        return queryResult.FirstOrDefault();
    }
    
    public async Task<List<ReplacementWord>> GetAllAsync(int skip, int take)
    {
        var queryCommandBuilder = _queryCommandBuilderFactory.Create();

        await using var connection = (PostgresConnection) _connectionFactory.Create(ConnectionString);
        
        queryCommandBuilder
            .SetConnection(connection)
            .SetQuery(
                "SELECT * FROM public.\"replacement_words\" LIMIT ($1) OFFSET ($2);")
            .AddParameter(take)
            .AddParameter(skip);
        
        await using var command = queryCommandBuilder.Build();
        var queryResult = await _queryService.ExecuteReaderAsync<IQueryCommand, ReplacementWord>(command);
        return queryResult.ToList();
    }

    public async Task<bool> UpdateAsync(ReplacementWord model)
    {
        var queryCommandBuilder = _queryCommandBuilderFactory.Create();

        await using var connection = (PostgresConnection) _connectionFactory.Create(ConnectionString);
        
        queryCommandBuilder
            .SetConnection(connection)
            .SetQuery(
                @"UPDATE public.""replacement_words""
                SET oldspeak = ($1)::text,
                    newspeak = ($2)::text
                WHERE id = ($3)::uuid;")
            .AddParameter(model.Oldspeak)
            .AddParameter(model.Newspeak)
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
                FROM public.""replacement_words""
                WHERE id = ($1)::uuid;")
            .AddParameter(id);
        
        await using var command = queryCommandBuilder.Build();
        var queryResult = await _queryService.ExecuteNonQueryAsync(command);
        return queryResult == 1;
    }
}