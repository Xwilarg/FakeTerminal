using System.Diagnostics.CodeAnalysis;

namespace FakeTerminal.Parsing;

public class Parameter
{
    public string Name { set; get; }
    public string Value { set; get; }

    public CommandParameter RefParameter { set; get; }
}

class ParameterEqualityComparer : IEqualityComparer<Parameter>
{
    public bool Equals(Parameter? x, Parameter? y)
    {
        if (x is null) return y is null;
        if (y is null) return false;

        return x.Name == y.Name;
    }

    public int GetHashCode([DisallowNull] Parameter obj)
    {
        if (obj.Name is null) return 0;
        return obj.Name.GetHashCode();
    }
}