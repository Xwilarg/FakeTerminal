using FakeTerminal.Parsing;
using FakeTerminal.Parsing.Impl;

namespace FakeTerminal;

public class Client
{
    internal Client(DirectoryInfo currentDir)
    {
        CurrentDir = currentDir;

        _commands = new ACommand[] {
            new LsCommand()
        }.ToDictionary(x => x.Name, x => x);
    }

    private Dictionary<string, ACommand> _commands;

    public string ParseCommand(string rawCmd)
    {
        rawCmd = rawCmd.Trim();
        if (rawCmd == null) throw new NullReferenceException();

        var words = rawCmd.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var cmd = words[0].ToLowerInvariant();

        if (_commands.TryGetValue(cmd, out var value))
        {
            var retValue = value.DoAction(this, out var output);
            return output;
        }
        return "Command not found";
    }

    internal DirectoryInfo CurrentDir;
}