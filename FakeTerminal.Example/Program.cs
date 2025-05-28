using FakeTerminal;

var term = new TerminalInstance(".");
var client = term.CreateClient();

void ShowCommand(string data)
{
    Console.WriteLine($"Running command \"{data}\"");
    var output = client.ParseCommand(data, out var parameters);
    Console.WriteLine($"Parameters: {string.Join(", ", parameters.Select(x => $"{x.Name}: {x.Value}"))}");
    Console.WriteLine($"Current folder: {client.CurrentDir.FullName}");
    Console.WriteLine($"Output: {output}");
    Console.WriteLine();
}

ShowCommand("ls -la");
ShowCommand("ls --target test");
ShowCommand("ls --target some\\ folder");
ShowCommand("ls --target \"some folder\"");
ShowCommand("ls --target folder other");