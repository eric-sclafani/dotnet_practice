using CommandLine;
using Game.Models;
using Game.Modules.World;

namespace Game;

internal class Program
{
    private class Options
    {
        [Option('r', "rows", Required = false, Default = 5, HelpText = "# of rows for World Map.")]
        public int Rows { get; }

        [Option('c', "cols", Required = false, Default = 5, HelpText = "# of cols for World Map.")]
        public int Cols { get; }

        public Options(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
        }
    }

    private static GameArgs GetArgs(string[] args)
    {
        var gameArgs = new GameArgs();
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(o =>
            {
                gameArgs.Rows = o.Rows;
                gameArgs.Cols = o.Cols;
            });

        gameArgs.ValidateArgs();
        return gameArgs;
    }

    private static void Main(string[] args)
    {
        // eventually maybe: https://github.com/shibayan/Sharprompt

        var gameArgs = GetArgs(args);
        var rows = gameArgs.Rows;
        var cols = gameArgs.Cols;

        Player player = new("Eric", 30, 1, 8);
        World world = new(rows, cols);


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