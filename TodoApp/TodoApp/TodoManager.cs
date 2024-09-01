using Spectre.Console;
using TodoApp.DB;

namespace TodoApp;

using Models;

public class TodoManager
{
    public TodoManager()
    {
        DBManager.InitTable();
    }

    public void InterpretUserInput(string input)
    {
        var actions = new Dictionary<string, Action>
        {
            { "Add New", Add },
            { "View All", View },
            { "Edit", Edit },
            { "Delete", Delete },
            { "Clear", Clear },
            { "Exit", () => Environment.Exit(0) }
        };

        if (actions.TryGetValue(input, out var value))
        {
            value();
        }
        else
        {
            Console.WriteLine($"Command '{input}' not recognized");
        }
    }

    private static string GetUserInput(string helpText = "")
    {
        Console.Write($"{helpText}>>> ");
        var userInput = Console.ReadLine().Trim().ToLower();
        return userInput;
    }

    private static void Add()
    {
        // TODO: make this a loop until the user wants to stop adding todos

        ShowRule("[red]Adding new todo[/]");
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

        var result = DBManager.Insert(todo);
        AnsiConsole.MarkupLine(result == 1
            ? "[green]Success![/] Your todo has been added."
            : "[red]Oops! Something went wrong[/]");
        View();
    }

    private static void View()
    {
        var grid = new Grid();
        grid.AddColumns(4);
        grid.AddRow([
            new Text("Todo ID", new Style(Color.Fuchsia, Color.Black)).Centered(),
            new Text("Completed", new Style(Color.Red, Color.Black)).LeftJustified(),
            new Text("Description", new Style(Color.Green, Color.Black)).Centered(),
            new Text("Due date", new Style(Color.Blue, Color.Black)).RightJustified()
        ]);

        var todos = DBManager.View();
        foreach (var todo in todos)
        {
            grid.AddRow(
                new Text(todo.ID.ToString()),
                new Text(todo.IsCompleted == 1 ? "[X]" : "[ ]"),
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
        // if (_todos.Count > 0)
        // {
        //     ShowRule("[red]Editing todo[/]");
        //     View();
        //     AnsiConsole.MarkupLine("Select todo ID");
        //
        //     var userSelection = GetUserTodoSelection();
        //     Console.WriteLine($"You chose todo {userSelection.ID} to edit!!!");
        // }
        // else
        // {
        //     AnsiConsole.MarkupLine("\nYou have [fuchsia]no todos to edit.[/]\n");
        // }
    }

    private void Delete()
    {
        // TODO: add auto incrementing to DB table so IDS can update automatically
    }

    private void Clear()
    {
        // _todos.Clear();
        // AnsiConsole.MarkupLine("Your todos have been [blue]cleared[/]");
    }

    private static void ShowRule(string text)
    {
        var rule = new Rule(text)
        {
            Justification = Justify.Left
        };

        AnsiConsole.Write(rule);
    }

    // private Todo? GetTodoById(int ID)
    // {
    //     return _todos.FirstOrDefault(todo => todo.ID == ID, null);
    // }
    //
    // private Todo GetUserTodoSelection()
    // {
    //     do
    //     {
    //         var userInput = GetUserInput();
    //         if (int.TryParse(userInput, out var number))
    //         {
    //             var todo = GetTodoById(number);
    //             if (todo is null)
    //             {
    //                 AnsiConsole.MarkupLine($"[red]Could not find todo number '{number}'[/]");
    //             }
    //             else
    //             {
    //                 return todo;
    //             }
    //         }
    //         else
    //         {
    //             AnsiConsole.MarkupLine($"[red]Invalid input: '{userInput}'. Must be a number.[/]");
    //         }
    //     } while (true);
    // }
}