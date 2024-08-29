using Spectre.Console;
using TodoApp;


internal class Program
{
    public static void Main()
    {
        DisplayInitMessage();

        var userInput = "";
        var todoManager = new TodoManager();

        while (userInput != "Exit")
        {
            userInput = GetUserInput();
            todoManager.InterpretUserInput(userInput);
        }
    }

    private static void DisplayInitMessage()
    {
        Console.WriteLine(
            "  ______          __         __    _      __ \n /_  __/___  ____/ /___     / /   (_)____/ /_\n  / / / __ \\/ __  / __ \\   / /   / / ___/ __/\n / / / /_/ / /_/ / /_/ /  / /___/ (__  ) /_  \n/_/  \\____/\\__,_/\\____/  /_____/_/____/\\__/"
        );
        Console.WriteLine("\nWelcome to your todo list. What would you like to do?");
    }

    private static string GetUserInput()
    {
        var userInput = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices([
                    "Add New Todo",
                    "View All Todos",
                    "Edit Todo",
                    "Delete Todo",
                    "Exit"
                ])
                .EnableSearch()
                
            );
        return userInput;
    }
}