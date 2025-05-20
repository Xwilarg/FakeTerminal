namespace FakeTerminal;

public class Client
{
    internal Client(DirectoryInfo currentDir)
    {
        _currentDir = currentDir;
    }

    public void ParseCommand(string rawCmd)
    {
        rawCmd = rawCmd.Trim();
        if (rawCmd == null) throw new NullReferenceException();

        var words = rawCmd.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        switch
    }

    private DirectoryInfo _currentDir;
}