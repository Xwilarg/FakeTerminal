namespace FakeTerminal.Parsing;

internal record CommandParameter
{
    internal char? ShortName { set; get; }
    internal string LongName { set; get; }
    internal string Description { set; get; }
    internal bool Mandatory { set; get; }
}
