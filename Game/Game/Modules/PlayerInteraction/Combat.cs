using Game.Models;
using Spectre.Console;

namespace Game.Modules.PlayerInteraction;

public class Combat : IPlayerInteraction
{
    private readonly Player _player;
    private readonly Enemy _enemy;
    public bool Resolved { get; set; }

    public void Begin()
    {
        var enemyName = $"[red]{_enemy.Name}[/]";
        AnsiConsole.Markup($"You have entered a battle with a {enemyName}! Prepare for battle!");
        while (_enemy.IsAlive() && _player.IsAlive())
        {
            var userInput = Player.playerInput();
            if (userInput == "a")
            {
                var playerDmg = _player.DealDamage();
                _enemy.ReceiveDamage(playerDmg);
                Console.WriteLine($"\nYou deal [blue]{playerDmg}[/] damage to the {enemyName}.");
            }

            var enemyDmg = _enemy.DealDamage();
            _player.ReceiveDamage(enemyDmg);
            Console.WriteLine($"The {enemyName} deals {enemyDmg} to you.");
            DisplayStats();
        }

        Resolved = true;
        DisplayWinner();
    }

    private void DisplayWinner()
    {
        var winner = _player.IsAlive() ? "player" : "enemy";
        var msg = winner switch
        {
            "player" => $"You have defeated the {_enemy.Name}!",
            _ => $"You have been defeated by the {_enemy.Name}. You have failed!"
        };
        Console.WriteLine(msg);
    }

    private void DisplayStats()
    {
        var tildes = new string('~', 30);
        var msg =
            $"\n{tildes}\nYour health: {_player.CurrentHealth}\n{_enemy.Name} health: {_enemy.CurrentHealth}\n{tildes}\n";
        Console.WriteLine(msg);
    }
}