
using Spectre.Console;


internal class Program
{
    public static void Main()
    {
        DisplayInitMessage();
        var userInput = "";
        while (userInput != "Exit")
        {
        }
    }

    private static void DisplayInitMessage()
    {
        Console.WriteLine(
            "  ______          __         __    _      __ \n /_  __/___  ____/ /___     / /   (_)____/ /_\n  / / / __ \\/ __  / __ \\   / /   / / ___/ __/\n / / / /_/ / /_/ / /_/ /  / /___/ (__  ) /_  \n/_/  \\____/\\__,_/\\____/  /_____/_/____/\\__/"
        );
        Console.WriteLine(
            "\nWelcome to your todo list. What would you like to do?"
        );

    }

   
    
}