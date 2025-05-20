using FakeTerminal;

var term = new TerminalInstance(".");
var client = term.CreateClient();

// See https://aka.ms/new-console-template for more information
Console.WriteLine(client.CurrentDir);
