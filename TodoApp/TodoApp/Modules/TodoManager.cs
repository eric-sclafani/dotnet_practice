using Spectre.Console;
using TodoApp.DB;

namespace TodoApp;

using Models;

public static class TodoManager
{
    public static void InterpretUserInput(string input)
    {
        var actions = new Dictionary<string, Action>
        {
            { "Add New", Add },
            { "View All", View },
            { "Edit", Edit },
            { "Delete", Delete },
            {"Clear All", Clear },
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

    private static void Add()
    {
        ShowRule("[red]Adding new todo[/]");
        var todo = new Todo();
        todo.SetDescription();
        todo.SetDate();

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
            new Text("Todo ID", new Style(Color.Fuchsia, Color.Black)).LeftJustified(),
            new Text("Completed", new Style(Color.Red, Color.Black)).LeftJustified(),
            new Text("Description", new Style(Color.Green, Color.Black)).LeftJustified(),
            new Text("Due date", new Style(Color.Blue, Color.Black)).LeftJustified()
        ]);

        var todos = DBManager.QueryTodos();
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

    private static void Edit()
    {
        var todos = DBManager.QueryTodos();
        if (todos.Count > 0)
        {
            ShowRule("[red]Editing todo[/]");
            View();
            AnsiConsole.MarkupLine("[green]Select ID[/]");
            var userSelection = GetUserTodoSelection();

            Console.WriteLine($"You chose to edit {userSelection}");
        }
        else
        {
            AnsiConsole.MarkupLine("\nYou have [fuchsia]no todos to edit.[/]\n");
        }
    }

    private static void Delete()
    {
        var todos = DBManager.QueryTodos();
        if (todos.Count > 0)
        {
            View();
            ShowRule("[red]Deleting todo[/]");
            AnsiConsole.MarkupLine("[green]Select ID[/]");
            var userSelection = GetUserTodoSelection();

            var result = DBManager.Delete(userSelection.ID);
            AnsiConsole.MarkupLine(result == 1
                ? "[green]Success![/] Todo item deleted."
                : "[red]Oops! Something went wrong[/]");

            View();
        }
        else
        {
            AnsiConsole.MarkupLine("\nYou have [fuchsia]no todos to delete.[/]\n");
        }
    }
    
    private static void Clear()
    {

       var prompt = AnsiConsole.Prompt(
           new ConfirmationPrompt("[red]WARNING[/]: you are about to delete [red]ALL[/] todos. Proceed?")  
               .ShowChoices()
               .ShowDefaultValue()
       );
       if (!prompt) return;
       
       DBManager.Clear();
       DBManager.MaybeInitTable();
       View();
    }

    private static void ShowRule(string text)
    {
        var rule = new Rule(text)
        {
            Justification = Justify.Left
        };

        AnsiConsole.Write(rule);
    }

    private static Todo? GetTodoById(int ID)
    {
        var todos = DBManager.QueryTodos();
        return todos.FirstOrDefault(todo => todo.ID == ID, null);
    }

    private static Todo GetUserTodoSelection()
    {
        do
        {
            var userInput = Utils.GetUserInput();
            if (int.TryParse(userInput, out var number))
            {
                var todo = GetTodoById(number);
                if (todo is null)
                {
                    AnsiConsole.MarkupLine($"[red]Could not find todo number '{number}'[/]");
                }
                else
                {
                    return todo;
                }
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Invalid input: '{userInput}'. Must be a number.[/]");
            }
        } while (true);
    }
}