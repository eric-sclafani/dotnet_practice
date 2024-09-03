using Spectre.Console;
using TodoApp.DB;
using TodoApp;


internal class Program
{
    public static void Main()
    {
        DisplayInitMessage();
        var userInput = "";
        DBManager.MaybeInitTable();

        while (userInput != "Exit")
        {
            userInput = SelectAction();
            Console.Clear();
            TodoManager.InterpretUserInput(userInput);
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
        var userInput = Utils.SelectionFromChoices([
            "Add New",
            "View All",
            "Edit",
            "Delete",
            "Clear All",
            "Exit"
        ]);
                
        return userInput;
    }
}