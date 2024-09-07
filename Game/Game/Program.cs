using Game.Models;
using Game.Modules.World;

namespace Game;

internal class Program
{
    
    private static void Main(string[] args)
    {
        // move to title screen eventually
        const int rows = 8;
        const int cols = 7;

        Player player = new("Eric", 30, 1, 8);
        World world = new(rows, cols);

        world.DisplayGrid();
        
        var playerInput = "";
        while (playerInput != "exit")
        {
            Console.WriteLine("Where would you like to go?");
            playerInput = Player.playerInput();

            var playerMoved = world.ChangePlayerPosition(playerInput);
            if (playerMoved)
            {
                world.DisplayGrid();
            }
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