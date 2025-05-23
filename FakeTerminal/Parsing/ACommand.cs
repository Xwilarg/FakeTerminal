namespace FakeTerminal.Parsing;

public abstract class ACommand
{
    protected ACommand(string name)
    {
        Name = name;
    }

    public abstract bool DoAction(Client client, out string output);

    public abstract string Description { get; }

    public string Name { private set; get; }
}