using DatabaseLib.Models;

namespace DatabaseLib.Services;

public interface IQueryService 
{
    Task<int> ExecuteNonQueryAsync<TCommand>(TCommand command) 
        where TCommand : IQueryCommand;

    Task<ICollection<T>> ExecuteReaderAsync<TCommand, T>(TCommand command)
        where TCommand : IQueryCommand 
        where T : class;
}