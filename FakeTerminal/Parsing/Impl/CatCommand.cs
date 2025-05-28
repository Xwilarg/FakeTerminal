namespace FakeTerminal.Parsing.Impl;

public class CatCommand : ACommand
{
    public CatCommand() : base("cat")
    { }

    public override string Description => "Show the content of a file";

    internal override CommandParameter[] AllParameters => [
        new()
        {
            ShortName = null,
            LongName = null,
            Description = "File to show",
            Mandatory = true
        }
    ];

    public override bool DoAction(Client client, Parameter[] parameters, out string output)
    {
        var res = base.DoAction(client, parameters, out output);
        if (!res) return false;

        var file = parameters.First(x => x.RefParameter.LongName == null);
        var path = Path.Combine(client.CurrentDir.FullName, file.Value);
        if (File.Exists(path) && client.IsPathValid(path))
        {
            output = File.ReadAllText(path);
            return true;
        }

        output = "The target file doesn't exists";
        return false;
    }
}
