using Spectre.Console;

namespace TodoApp.Models;

public record Todo
{
    public int ID { get; set; }
    public string Description;
    public int IsCompleted { get; init; }
    public string? DueDate { get; set; } = "";

    public void SetDescription()
    {
        var isValid = false;
        do
        {
            var text = Utils.GetUserInput("[green]Enter Description[/]");
            if (text != "")
            {
                isValid = true;
                Description = text;
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Incorrect description format.[/]");
            }
        } while (!isValid);
    }

    public void SetDate()
    {
        var isValid = false;
        do
        {
            var text = Utils.GetUserInput("[green]Enter Due Date[/] (MM/DD/YYYY or leave blank)");
            var isParsed = DateOnly.TryParse(text, out var dueDate);

            if (isParsed || text == "")
            {
                isValid = true;
                DueDate = text == "" ? "" : dueDate.ToString();
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Incorrect date format[/]");
            }
        } while (!isValid);
    }
}