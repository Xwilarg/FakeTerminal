using FakeTerminal;

var term = new TerminalInstance(".");
var client = term.CreateClient();

Console.WriteLine("Running command \"ls\"");
Console.WriteLine(client.ParseCommand("ls"));
