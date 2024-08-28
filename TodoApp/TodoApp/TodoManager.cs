using Spectre.Console;

namespace TodoApp;

using Models;

public class TodoManager
{
    private List<Todo> _todos = [];

    public void InterpretUserInput(string input)
    {
        AnsiConsole.MarkupLine($"\nYou've chosen to [aqua]{input}[/].");
        switch (input)
        {
            case "Add New Todo":
                Add();
                break;

            case "View All Todos":

                break;
            case "Edit Todo":

                break;

            case "Delete Todo":

                break;

            case "Exit":
                Environment.Exit(0);
                break;
        }
    }

    private void Add()
    {
        var todo = new Todo();
        DisplayRule("Description");
        var desc = AnsiConsole.Prompt(new TextPrompt<string>("Enter Todo Description:"));

        var dueDate = AnsiConsole.Prompt(
            new TextPrompt<DateOnly?>("Enter Due Date (MM/DD/YYYY or leave blank):")
                .AllowEmpty()
                .DefaultValue(null)
                .ShowDefaultValue(false)
        );

        todo.Description = desc;
        todo.DueDate = dueDate;
        _todos.Add(todo);
        AnsiConsole.MarkupLine("\n[green]Success![/] Added your new todo item.");
    }

    private void View()
    {
    }

    private void Edit()
    {
    }

    private void Delete()
    {
    }

    private static void DisplayRule(string text)
    {
        var rule = new Rule($"[red]{text}[/]");
        rule.LeftJustified();
        Console.WriteLine();
        AnsiConsole.Write(rule);
    }
}