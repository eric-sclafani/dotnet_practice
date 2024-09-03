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

    public static string SelectionFromChoices(string[] choices)
    {
        var userInput = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(choices)
                .EnableSearch()
        );
        return userInput;
    }

    public static bool YesNoPrompt(string text)
    {
        var prompt = AnsiConsole.Prompt(
            new ConfirmationPrompt(text)  
                .ShowChoices()
                .ShowDefaultValue()
        );
        return prompt;
    }
}