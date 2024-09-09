namespace Game.Models;

public class Player : Character
{
    public Player(string name, int maxHealth, int minDmg, int maxDmg) : base(name, maxHealth, minDmg, maxDmg)
    {
    }

    public override string ToString()
    {
        return $"[green]{Name}[/]";
    }

    public static string? playerInput()
    {
        var input = Console.ReadLine()?.Trim().ToLower();
        return input;
    }
    // TODO: add a blocking mechanic somehow, same with enemy (game randomly chooses whether enemy blocks or attacks?)
}