using FakeTerminal.Parsing;
using FakeTerminal.Parsing.Impl;

namespace FakeTerminal;

public class Client
{
    internal Client(DirectoryInfo currentDir)
    {
        BaseDir = currentDir;
        CurrentDir = currentDir;

        _commands = new ACommand[] {
            new LsCommand(),
            new CdCommand(),
            new CatCommand()
        }.ToDictionary(x => x.Name, x => x);
    }

    private Dictionary<string, ACommand> _commands;

    public string ParseCommand(string rawCmd, out Parameter[] parameters)
    {
        rawCmd = rawCmd.Trim();
        if (rawCmd == null) throw new NullReferenceException();

        var words = rawCmd.Split(' ');
        var cmd = words[0].ToLowerInvariant();
        parameters = new ParameterParser().Parse(rawCmd[cmd.Length..]);

        if (_commands.TryGetValue(cmd, out var value))
        {
            var retValue = value.DoAction(this, parameters, out var output);
            return output;
        }
        return "Command not found";
    }

    internal bool IsPathValid(string path)
        => Path.GetFullPath(path).StartsWith(BaseDir.FullName);

    public DirectoryInfo CurrentDir { internal set; get; }
    internal DirectoryInfo BaseDir { private set; get; }
}