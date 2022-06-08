using DatabaseLib.Models;

namespace DatabaseLib.Services;

public class MappingService
{
    public readonly Dictionary<Type, ClassMap> ClassMapping;

    public MappingService()
    {
        ClassMapping = new Dictionary<Type, ClassMap>();
    }

    public void RegisterClass<T>(params Tuple<string, string>[] mappings)
    {
        var propertyMapping = new Dictionary<string, string>();
        foreach (var (classProperty, dbColumn) in mappings)
        {
            propertyMapping.Add(classProperty, dbColumn);
        }

        ClassMapping.Add(typeof(T), new ClassMap(propertyMapping));
    } 
}