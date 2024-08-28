namespace TodoApp.Models;

public record Todo
{
    public string Description { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateOnly? DueDate { get; set; }
}