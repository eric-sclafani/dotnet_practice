using Game.Models;
using Game.Modules.ConsoleIO;
using Game.Modules.Interaction;

namespace Game.Modules.World;

public class World
{
    private readonly Player _player;

    private readonly int _rows;
    private readonly int _cols;
    private readonly int _pointInteractionThreshold;

    public readonly Point[,] Grid;
    public Point PlayerPosition { get; private set; }

    public World(int rows, int cols, int thres, Player player)
    {
        _rows = rows;
        _cols = cols;
        _pointInteractionThreshold = thres;
        _player = player;

        Grid = InitGrid();
        PlayerPosition = SetPlayerStartPosition();
    }

    public void ChangePlayerPosition(string? playerInput)
    {
        var newPos = GetNewCoord(playerInput);
        try
        {
            var newX = PlayerPosition.X + newPos.x;
            var newY = PlayerPosition.Y + newPos.y;
            PlayerPosition = Grid[newX, newY];
            Output.DisplayGrid(Grid, PlayerPosition);
            
            var combat = PlayerPosition.Combat;
            if (combat is not null && !combat.Resolved)
            {
                var enemy = new Enemy("Testie the Test Goblin", 10, 2, 5);
                combat.Player = _player;
                combat.Enemy = enemy;
                combat.Begin();
            }
        }
        catch
        {
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