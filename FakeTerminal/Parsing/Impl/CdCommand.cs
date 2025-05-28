namespace FakeTerminal.Parsing.Impl;

public class CdCommand : ACommand
{
    public CdCommand() : base("cd")
    { }

    public override string Description => "Show the content of a file";

    internal override CommandParameter[] AllParameters => [
        new()
        {
            ShortName = null,
            LongName = null,
            Description = "File to go to",
            Mandatory = false
        }
    ];

    public override bool DoAction(Client client, Parameter[] parameters, out string output)
    {
        var res = base.DoAction(client, parameters, out output);
        if (!res) return false;

        var path = parameters.FirstOrDefault(x => x.RefParameter.LongName == null);
        if (path == null)
        {
            client.CurrentDir = client.BaseDir;
            return true;
        }

        var newFolder = Path.Combine(client.CurrentDir.FullName, path.Value);

        if (Directory.Exists(newFolder) && Path.GetFullPath(newFolder).StartsWith(client.BaseDir.FullName)) // Is path valid and in our current workspace?
        {
            client.CurrentDir = new DirectoryInfo(newFolder);
            return true;
        }

        output = "The target directory doesn't exists";
        return false;
    }
}
