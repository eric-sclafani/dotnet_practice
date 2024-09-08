namespace Game.Models;

public class Player : Character
{
    // TODO: determines whether to display combat help text (?)
    public bool IsFirstCombat { get; set; }
    
    public Player(string name, int maxHealth, int minDmg, int maxDmg) : base(name, maxHealth, minDmg, maxDmg)
    {
    }

    public static string? playerInput()
    {
        var input = Console.ReadLine()?.Trim().ToLower();
        return input;
    }
    // TODO: add a blocking mechanic somehow, same with enemy (game randomly chooses whether enemy blocks or attacks?)
}