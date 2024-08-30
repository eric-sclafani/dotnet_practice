using Spectre.Console;
using Spectre.Console.Rendering;

namespace TodoApp;

using Models;

public class TodoManager
{
    private readonly List<Todo> _todos = [];

    public void InterpretUserInput(string input)
    {
        switch (input)
        {
            case "Add New Todo":
                Add();
                break;

            case "View All Todos":
                View();
                break;

            case "Edit Todo":
                Edit();
                break;

            case "Mark Completed":
                MarkCompleted();
                break;

            case "Delete Todo":
                Delete();
                break;

            case "Exit":
                Environment.Exit(0);
                break;
        }
    }

    private void Add()
    {
        var todo = new Todo();
        var desc = AnsiConsole.Prompt(new TextPrompt<string>("Enter Todo Description:"));
        var dueDate = AnsiConsole.Prompt(
            new TextPrompt<DateOnly?>("Enter Due Date (MM/DD/YYYY or leave blank):")
                .DefaultValue(null)
                .ShowDefaultValue(false)
        );

        todo.Description = desc;
        todo.DueDate = dueDate.ToString();
        _todos.Add(todo);
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
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(_todos.Select((todo) => todo.Description))
                    .Mode(SelectionMode.Independent)
            );

            Console.WriteLine($"You chose {selection}");
        }
        else
        {
            Console.WriteLine("\nYou have no todos to edit.\n");
        }
    }

    private void MarkCompleted()
    {
    }

    private void Delete()
    {
    }
}