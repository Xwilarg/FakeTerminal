using System.Text;

namespace FakeTerminal.Parsing.Impl;

public class LsCommand : ACommand
{
    public LsCommand() : base("ls")
    { }

    public override string Description => "Display the list of files and folders in the current directory";

    private static string FormatFileName(string name)
    {
        if (name.Contains(' ')) return $"\"{name}\"";
        return name;
    }
    public override bool DoAction(Client client, out string output)
    {
        List<string> files = [];

        foreach (var d in Directory.GetDirectories(client.CurrentDir.FullName))
        {
            DirectoryInfo di = new(d);
            files.Add(FormatFileName(di.Name));
        }
        foreach (var f in Directory.GetFiles(client.CurrentDir.FullName))
        {
            FileInfo fi = new(f);
            files.Add(FormatFileName(fi.Name));
        }

        output = string.Join(" ", files);

        return true;
    }
}