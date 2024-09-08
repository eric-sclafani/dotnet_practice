namespace Game.Modules.PlayerInput;

using Spectre.Console;

public static class Input
{
    public static string GetDirection()
    {
        List<string> options = ["UpArrow", "DownArrow", "LeftArrow", "RightArrow"];
        string direction;
        do
        {
            AnsiConsole.MarkupLine("Use your arrow keys [green](\u2190 \u2191 \u2192 \u2193)[/] to move");
            direction = Console.ReadKey(true).Key.ToString();
        } while (!options.Contains(direction));

        return direction;
    }

    // public static string GetCombatOption()
    // {
    //     
    // }
}