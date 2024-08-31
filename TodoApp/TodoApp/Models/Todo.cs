namespace TodoApp.Models;

public record Todo
{
    public string Description;
    public bool IsCompleted { get; set; } = false;
    public string? DueDate { get; set; } = "";

    public int SetDescription(string text)
    {
        if (text.Trim() == "")
        {
            Console.WriteLine("Incorrect description format");
            return 0;
        }

        Description = text;
        return 1;
    }

    public int SetDate(string text)
    {
        if (text == "") return 1;

        var isParsed = DateTime.TryParse(text, out var date);

        if (!isParsed)
        {
            Console.WriteLine("Incorrect date format");
            return 0;
        }

        DueDate = date.ToString();
        return 1;
    }
}