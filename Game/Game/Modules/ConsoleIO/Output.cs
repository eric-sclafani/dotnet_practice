using Game.Models;
using Spectre.Console;

namespace Game.Modules.ConsoleIO;

public static class Output
{
    public static void BeginCombat(Player player, Enemy enemy)
    {
        AnsiConsole.MarkupLine($"{player} has entered a battle with {enemy}! Prepare for battle!");
    }

    public static void ReceiveDamage(Character dmgFrom, Character dmgTo, int amount)
    {
        AnsiConsole.MarkupLine($"{dmgFrom} deals [blue]{amount}[/] damage to {dmgTo}.");
    }

    // TODO: look into replacing with spectre bar chart (live display?)
    public static void DisplayCombatStats(Player player, Enemy enemy)
    {
        var tildes = new string('~', 30);
        var msg =
            $"\n{tildes}\nYour health: {player.CurrentHealth}\n{enemy.Name} health: {enemy.CurrentHealth}\n{tildes}\n";
        Console.WriteLine(msg);
    }

    public static void DisplayCombatWinner(Player player, Enemy enemy)
    {
        var winner = player.IsAlive() ? "player" : "enemy";
        var msg = winner switch
        {
            "player" => $"{player} has defeated {enemy}!",
            _ => $"{player.Name} has been defeated by the {enemy.Name}!"
        };
        AnsiConsole.MarkupLine(msg);
    }

    public static void DisplayGrid(Point[,] grid, Point playerPosition)
    {
        Console.Clear();
        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);
        
        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                var point = grid[row, col];

                if (point.Combat is not null && !point.Combat.Resolved && point.IsEqualTo(playerPosition))
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                else if (point.IsEqualTo(playerPosition))
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                }
                else if (point.Combat is not null && !point.Combat.Resolved)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.Write(point);
                Console.ResetColor();
            }

            Console.Write("\n");
        }
    }
}