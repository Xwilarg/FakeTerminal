namespace FakeTerminal.Parsing;

public record CommandParameter
{
    public char? ShortName { set; get; }
    public string? LongName { set; get; }
    public string Description { set; get; }
    public bool Mandatory { set; get; }
}
