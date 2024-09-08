using Game.Models;
using Spectre.Console;

namespace Game.Modules.PlayerInteraction;

public class Combat : IPlayerInteraction
{
    public Player Player { get; set; }
    public Enemy Enemy { get; set; }
    public bool Resolved { get; set; }

    public void Begin()
    {
        if (Enemy is not null && Player is not null)
        {
            var playerName = $"[green]{Player.Name}[/]";
            var enemyName = $"[red]{Enemy.Name}[/]";
            AnsiConsole.MarkupLine($"{playerName} have entered a battle with a {enemyName}! Prepare for battle!");

            while (Enemy.IsAlive() && Player.IsAlive())
            {
                var userInput = Player.playerInput();
                Console.Clear();
                if (userInput == "a")
                {
                    var playerDmg = Player.DealDamage();
                    Enemy.ReceiveDamage(playerDmg);
                    AnsiConsole.MarkupLine($"{playerName} deals [blue]{playerDmg}[/] damage to the {enemyName}.");
                }

                var enemyDmg = Enemy.DealDamage();
                Player.ReceiveDamage(enemyDmg);
                AnsiConsole.MarkupLine($"The {enemyName} deals [blue]{enemyDmg}[/] to {playerName}.");

                DisplayStats();
            }

            Resolved = true;
            DisplayWinner();
        }
        else
        {
            Console.WriteLine("Error: Player or Enemy does not exist");
        }
    }

    private void DisplayWinner()
    {
        var winner = Player.IsAlive() ? "player" : "enemy";
        var msg = winner switch
        {
            "player" => $"You have defeated the {Enemy.Name}!",
            _ => $"You have been defeated by the {Enemy.Name}. You have failed!"
        };
        Console.WriteLine(msg);
    }

    private void DisplayStats()
    {
        var tildes = new string('~', 30);
        var msg =
            $"\n{tildes}\nYour health: {Player.CurrentHealth}\n{Enemy.Name} health: {Enemy.CurrentHealth}\n{tildes}\n";
        Console.WriteLine(msg);
    }
}