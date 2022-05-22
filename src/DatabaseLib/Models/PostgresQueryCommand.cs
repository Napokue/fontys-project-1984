using Npgsql;

namespace DatabaseLib.Models;

public class PostgresQueryCommand : IQueryCommand
{
    private readonly NpgsqlCommand _command;

    public static implicit operator NpgsqlCommand(PostgresQueryCommand command) => command._command;
    
    public PostgresQueryCommand(NpgsqlCommand command)
    {
        _command = command;
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
        
        _command.Dispose();
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        await _command.DisposeAsync().ConfigureAwait(false);
    }
}