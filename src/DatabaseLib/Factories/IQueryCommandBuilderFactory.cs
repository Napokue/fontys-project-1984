using DatabaseLib.Builders;

namespace DatabaseLib.Factories;

public interface IQueryCommandBuilderFactory<T> where T : IQueryCommandBuilder
{
    T Create();
}