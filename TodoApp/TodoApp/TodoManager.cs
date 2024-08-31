using Spectre.Console;

namespace TodoApp;

using Models;

public class TodoManager
{
    private readonly List<Todo> _todos = [];

    public void InterpretUserInput(string input)
    {
        switch (input)
        {
            case "Add New":
                Add();
                break;

            case "View All":
                View();
                break;

            case "Edit":
                Edit();
                break;

            case "Delete":
                Delete();
                break;

            case "Exit":
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine($"Command '{input}' not recognized");
                break;
        }
    }

    public static string GetUserInput(string helpText = "")
    {
        Console.Write($"{helpText}>>> ");
        var userInput = Console.ReadLine().Trim().ToLower();
        return userInput;
    }

    private void Add()
    {
        var rule = new Rule("[red]Adding new todo[/]")
        {
            Justification = Justify.Left
        };

        AnsiConsole.Write(rule);

        var todo = new Todo();

        int setDesc;
        do
        {
            var desc = GetUserInput("Enter Description ");
            setDesc = todo.SetDescription(desc);
        } while (setDesc != 1);

        int setDate;
        do
        {
            var date = GetUserInput("Enter Due Date (MM/DD/YYYY or leave blank) ");
            setDate = todo.SetDate(date);
        } while (setDate != 1);

        _todos.Add(todo);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Success! ");
        Console.ResetColor();
        Console.WriteLine("Your todo has bee added.");
    }

    private void View()
    {
        var grid = new Grid();
        grid.AddColumns(3);
        grid.AddRow([
            new Text("Completed", new Style(Color.Red, Color.Black)).LeftJustified(),
            new Text("Description", new Style(Color.Green, Color.Black)).Centered(),
            new Text("Due date", new Style(Color.Blue, Color.Black)).RightJustified()
        ]);

        foreach (var todo in _todos)
        {
            grid.AddRow(
                new Text(todo.IsCompleted ? "[X]" : "[ ]"),
                new Text(todo.Description),
                new Text(todo.DueDate)
            );
        }

        var panel = new Panel(grid)
        {
            Border = BoxBorder.Heavy
        };
        panel.Expand();
        AnsiConsole.Write(panel);
    }

    private void Edit()
    {
        if (_todos.Count > 0)
        {
        }
        else
        {
            Console.WriteLine("\nYou have no todos to edit.\n");
        }
    }

    private void Delete()
    {
    }
}