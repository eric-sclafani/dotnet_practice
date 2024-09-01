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
            userInput = SelectAction();
            Console.Clear();
            todoManager.InterpretUserInput(userInput);
        }
    }

    private static void DisplayInitMessage()
    {
        AnsiConsole.Write(
            new FigletText("Todo")
                .LeftJustified()
                .Color(Color.Blue)
            );
    }

    private static string SelectAction()
    {
        Console.WriteLine("What would you like to do?");
        var userInput = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices([
                    "Add New",
                    "View All",
                    "Edit",
                    "Delete",
                    "Clear",
                    "Exit"
                ])
                .EnableSearch()
        );
        return userInput;
    }
}