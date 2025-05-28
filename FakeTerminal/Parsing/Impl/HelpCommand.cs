namespace FakeTerminal.Parsing.Impl;

public class HelpCommand : ACommand
{
    public HelpCommand() : base("help")
    { }

    public override string Description => "Show information about a command";

    internal override CommandParameter[] AllParameters => [
        new()
        {
            ShortName = null,
            LongName = null,
            Description = "Command to get information about",
            Mandatory = false
        }
    ];

    public override bool DoAction(Client client, Parameter[] parameters, out string output)
    {
        var res = base.DoAction(client, parameters, out output);
        if (!res) return false;

        var cmd = parameters.FirstOrDefault(x => x.RefParameter.LongName == null);

        if (cmd != null)
        {
            var foundCmd = client.Commands.Values.FirstOrDefault(x => string.Compare(x.Name, cmd.Value, true) == 0);
            if (foundCmd != null)
            {
                output = $"{foundCmd.Name}: {foundCmd.Description}"; // TODO: Show arguments
                return true;
            }

            output = "The given command doesn't exists";
            return false;
        }

        output = string.Join("\n", client.Commands.Select(x => $"{x.Value.Name}: {x.Value.Description}"));
        return true;
    }
}
