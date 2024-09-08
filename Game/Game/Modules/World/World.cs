using Game.Models;
using Game.Modules.PlayerInteraction;

namespace Game.Modules.World;

public class World
{
    private readonly Player _player;

    private readonly int _rows;
    private readonly int _cols;
    private readonly Point[,] _grid;
    private readonly int _pointInteractionThreshold;
    private Point _playerPosition { get; set; }

    public World(int rows, int cols, int thres, Player player)
    {
        _rows = rows;
        _cols = cols;
        _pointInteractionThreshold = thres;
        _player = player;

        _grid = InitGrid();
        _playerPosition = SetPlayerStartPosition();
    }

    public void ChangePlayerPosition(string? playerInput)
    {
        var newPos = GetNewCoord(playerInput);
        try
        {
            var newX = _playerPosition.X + newPos.x;
            var newY = _playerPosition.Y + newPos.y;
            _playerPosition = _grid[newX, newY];
            DisplayGrid();

            var combat = _playerPosition.Combat;

            if (combat is not null && !combat.Resolved)
            {
                var enemy = new Enemy("Test Goblin", 10, 2, 5);
                combat.Player = _player;
                combat.Enemy = enemy;
                combat.Begin();
            }
        }
        catch
        {
            Console.WriteLine("You have reached the end of the map and cannot go that way.");
        }
    }

    public void DisplayGrid()
    {
        Console.Clear();
        for (var row = 0; row < _rows; row++)
        {
            for (var col = 0; col < _cols; col++)
            {
                var point = _grid[row, col];

                if (point.Combat is not null && !point.Combat.Resolved && point.IsEqualTo(_playerPosition))
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                else if (point.IsEqualTo(_playerPosition))
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

    private Point[,] InitGrid()
    {
        var grid = new Point[_rows, _cols];
        for (var row = 0; row < _rows; row++)
        for (var col = 0; col < _cols; col++)
        {
            Point point = new(row, col);

            if (PointGetsCombat() && !point.IsEqualTo(SetPlayerStartPosition()))
            {
                point.Combat = new Combat();
            }

            grid[row, col] = point;
        }

        return grid;
    }

    private Point SetPlayerStartPosition()
    {
        var x = _rows - 1;
        var y = (int)decimal.Round(_cols / 2);
        return new Point(x, y);
    }

    private static (int x, int y) GetNewCoord(string input)
    {
        (int x, int y) newPos = input switch
        {
            "UpArrow" => (-1, 0),
            "DownArrow" => (1, 0),
            "RightArrow" => (0, 1),
            "LeftArrow" => (0, -1),
            _ => (0, 0)
        };
        return newPos;
    }

    private bool PointGetsCombat()
    {
        var r = new Random();
        var chance = r.Next(1, 11);
        return chance <= _pointInteractionThreshold;
    }
}