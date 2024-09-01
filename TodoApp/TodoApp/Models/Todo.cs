using Spectre.Console;
namespace TodoApp.Models;

public record Todo
{
    public int ID { get; set; }
    public string Description;
    public bool IsCompleted { get; set; } = false;
    public string? DueDate { get; set; } = "";

    public int SetDescription(string text)
    {
        if (text.Trim() == "")
        {
            AnsiConsole.MarkupLine("[red]Incorrect description format.[/]");
            return 0;
        }

        Description = text;
        return 1;
    }

    public int SetDate(string text)
    {
        if (text == "") return 1;

        var isParsed = DateOnly.TryParse(text, out var date);

        if (!isParsed)
        {
            AnsiConsole.MarkupLine("[red]Incorrect date format[/]");
            return 0;
        }

        DueDate = date.ToString();
        return 1;
    }
}