using DatabaseLib.Models;

namespace DatabaseLib.Builders;

public interface IQueryCommandBuilder
{
    IQueryCommand Build();
}