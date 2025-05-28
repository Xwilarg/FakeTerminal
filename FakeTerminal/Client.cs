using FakeTerminal.Parsing;
using FakeTerminal.Parsing.Impl;

namespace FakeTerminal;

public class Client
{
    internal Client(DirectoryInfo currentDir)
    {
        BaseDir = currentDir;
        CurrentDir = currentDir;

        Commands = new ACommand[] {
            new LsCommand(),
            new CdCommand(),
            new CatCommand(),
            new HelpCommand()
        }.ToDictionary(x => x.Name, x => x);
    }

    internal Dictionary<string, ACommand> Commands { private set; get; }

    public string ParseCommand(string rawCmd, out Parameter[] parameters)
    {
        rawCmd = rawCmd.Trim();
        if (rawCmd == null) throw new NullReferenceException();

        var words = rawCmd.Split(' ');
        var cmd = words[0].ToLowerInvariant();
        parameters = new ParameterParser().Parse(rawCmd[cmd.Length..]);

        if (Commands.TryGetValue(cmd, out var value))
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