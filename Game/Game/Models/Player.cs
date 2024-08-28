namespace Game.Models;

public class Player : Character
{
    public Player(string name, int maxHealth, int minDmg, int maxDmg) : base(name, maxHealth, minDmg, maxDmg)
    {
    }

    public static string? playerInput()
    {
        var input = Console.ReadLine()?.Trim().ToLower();
        return input;
    }
}