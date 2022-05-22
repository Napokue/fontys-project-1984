namespace DatabaseLib.Models;

public class ClassMap
{
    public Dictionary<string, string> PropertyMapping { get; }

    public ClassMap(Dictionary<string, string> propertyMapping)
    {
        PropertyMapping = propertyMapping;
    }
}