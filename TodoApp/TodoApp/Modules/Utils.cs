using Spectre.Console;

namespace TodoApp;

public static class Utils
{
    public static string GetUserInput(string helpText = "")
    {
        AnsiConsole.Markup($"{helpText} >>> ");
        var userInput = Console.ReadLine().Trim().ToLower();
        return userInput;
    }
}