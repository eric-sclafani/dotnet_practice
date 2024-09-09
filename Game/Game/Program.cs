using Game.Models;
using Game.Modules.ConsoleIO;
using Game.Modules.World;

namespace Game;

internal class Program
{
    private static void Main()
    {
        // move to title screen eventually
        const int rows = 4;
        const int cols = 5;
        const int thres = 4;


        Player player = new("Eric", 30, 1, 8);
        World world = new(rows, cols, thres, player);

        var playerInput = "";
        while (playerInput != "exit" && player.IsAlive())
        {
            Output.DisplayGrid(world.Grid, world.PlayerPosition);
            Console.WriteLine("Where would you like to go?");
            playerInput = Input.GetDirection();

            world.ChangePlayerPosition(playerInput);

            
        }
    }
    
}


//! Systems to complete before halting development:
/*
 * (DONE) World traversal system
 ? World event system
 ? Fully featured Player class (inventory, stats, levels, experience, etc...)
 ? Win condition (start with a point system for now?)
 */