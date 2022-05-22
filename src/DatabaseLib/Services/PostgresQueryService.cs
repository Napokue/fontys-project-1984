using System.Data;
using System.Reflection;
using DatabaseLib.Models;
using Npgsql;

namespace DatabaseLib.Services;

public class PostgresQueryService : IQueryService
{
    private readonly MappingService _mappingService;
    
    public PostgresQueryService(MappingService mappingService)
    {
        _mappingService = mappingService;
    }

    public async Task<int> ExecuteNonQueryAsync<TCommand>(TCommand command) where TCommand : IQueryCommand
    {
        var postgresCommand = command as PostgresQueryCommand;
        var npgsqlCommand = (NpgsqlCommand) postgresCommand!;
        return await npgsqlCommand.ExecuteNonQueryAsync();
    }

    public async Task<ICollection<T>> ExecuteReaderAsync<TCommand, T>(TCommand command) where TCommand : IQueryCommand where T : class
    {
        var result = new List<T>(0);
        var postgresCommand = command as PostgresQueryCommand;
        var npgsqlCommand = (NpgsqlCommand) postgresCommand!;
        await using var reader = await npgsqlCommand.ExecuteReaderAsync();

        var schemaTable = await reader.GetSchemaTableAsync();
        var columnNameIndex = schemaTable.Columns.IndexOf("ColumnName");

        var columnNames = schemaTable.Rows.Cast<DataRow>().Select(x => x.ItemArray[columnNameIndex]!.ToString()).ToList();
        
        while (await reader.ReadAsync())
        {
            var instance = Activator.CreateInstance<T>();
            if (!_mappingService.ClassMapping.TryGetValue(typeof(T), out var classMap))
            {
                throw new Exception($"Class of type: {typeof(T)} does not have a mapping registered");
            }

            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var propName = propertyInfo.Name;
                if (!classMap.PropertyMapping.TryGetValue(propName, out var dbName))
                {
                    continue;
                }
                
                if (!columnNames.Contains(dbName))
                {
                    continue;
                }

                var value = reader[dbName];
                propertyInfo.SetValue(instance, value);
            }
            result.Add(instance);
        }

        return result;
    }
}