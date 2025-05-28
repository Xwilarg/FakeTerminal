using FakeTerminal;

var term = new TerminalInstance(".");
var client = term.CreateClient();

void ShowCommand(string data)
{
    Console.WriteLine($"Running command \"{data}\"");
    var output = client.ParseCommand(data, out var parameters);
    Console.WriteLine($"Parameters: {string.Join(", ", parameters.Select(x => $"{x.Name}: {x.Value}"))}");
    Console.WriteLine($"Current folder: {client.CurrentDir.FullName}");
    Console.WriteLine($"Output:\n{output}");
    Console.WriteLine();
}

if (!Directory.Exists("test")) Directory.CreateDirectory("test");

ShowCommand("ls");
ShowCommand("cd test");
ShowCommand("ls");
ShowCommand("cd ..");
ShowCommand("cd ..");
ShowCommand("ls -l");

ShowCommand("cd test");
ShowCommand("cd");

/*ShowCommand("ls -la");
ShowCommand("ls --target test");
ShowCommand("ls --target some\\ folder");
ShowCommand("ls --target \"some folder\"");
ShowCommand("ls --target folder other");*/