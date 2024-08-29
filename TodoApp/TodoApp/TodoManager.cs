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
            case "Add New Todo":
                Add();
                break;

            case "View All Todos":
                View();
                break;
            case "Edit Todo":
                Edit();
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
        todo.DueDate = dueDate.ToString() != "" ? dueDate.ToString() : null;
        _todos.Add(todo);
        View();
    }

    private void View()
    {
        var table = new Table
        {
            Title = new TableTitle("Todos")
        };
        table.AddColumns([
            new TableColumn("Description"),
            new TableColumn("Due Date")
        ]);

        foreach (var todo in _todos)
        {
            table.AddRow(todo.Description, todo.DueDate ?? "NA");
        }

        Console.WriteLine();
        AnsiConsole.Write(table);
        Console.WriteLine();
    }

    private void Edit()
    {
    }

    private void Delete()
    {
    }
}