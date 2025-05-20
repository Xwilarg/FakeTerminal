namespace FakeTerminal;

public class TerminalInstance
{
    public TerminalInstance(string baseDirPath)
    {
        _baseDir = new(baseDirPath);
    }

    public Client CreateClient()
    {
        return new Client(_baseDir);
    }

    private DirectoryInfo _baseDir;
}
