using Game.Models;
using Game.Modules.ConsoleIO;
using Game.Modules.PlayerInteraction;
using Spectre.Console;

namespace Game.Modules.Interaction;

public class Combat : IPlayerInteraction
{
    public Player Player { get; set; }
    public Enemy Enemy { get; set; }
    public bool Resolved { get; set; }

    public void Begin()
    {
        Output.BeginCombat(Player, Enemy);

        while (Enemy.IsAlive() && Player.IsAlive())
        {
            var userInput = Player.playerInput();
            if (userInput == "a")
            {
                var playerDmg = Player.DealDamage();
                Enemy.ReceiveDamage(playerDmg);
                Output.ReceiveDamage(Player, Enemy, playerDmg);
            }
            
            var enemyDmg = Enemy.DealDamage();
            Player.ReceiveDamage(enemyDmg);
            Output.ReceiveDamage(Enemy, Player, enemyDmg);

            Output.DisplayCombatStats(Player, Enemy);
        }

        Resolved = true;
        Output.DisplayCombatWinner(Player, Enemy);
    }

    

    
    
}