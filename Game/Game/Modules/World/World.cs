using Game.Modules.PlayerInteraction;

namespace Game.Modules.World;

public class World
{
    private readonly int _rows;
    private readonly int _cols;
    private readonly Point[,] _grid;
    private int _pointInteractionThreshold;
    private Point _playerPosition { get; set; }

    public World(int rows, int cols, int thres = 6)
    {
        _rows = rows;
        _cols = cols;
        _pointInteractionThreshold = thres;

        _grid = InitGrid();
        _playerPosition = SetPlayerStartPosition();
    }

    public bool ChangePlayerPosition(string? playerInput)
    {
        var newPos = GetNewCoord(playerInput);
        try
        {
            var newX = _playerPosition.X + newPos.x;
            var newY = _playerPosition.Y + newPos.y;
            _playerPosition = _grid[newX, newY];
            return true;
        }
        catch
        {
            Console.WriteLine("You have reached the end of the map and cannot go that way.");
            return false;
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

                if (point.Combat is not null && point == _playerPosition)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                else if (point == _playerPosition)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                }
                else if (point.Combat is not null)
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

            if (PointGetsCombat())
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
        return _grid[x, y];
    }

    private static (int x, int y) GetNewCoord(string input)
    {
        (int x, int y) newPos = input switch
        {
            "n" => (-1, 0),
            "s" => (1, 0),
            "e" => (0, 1),
            "w" => (0, -1),
            "ne" => (-1, 1),
            "nw" => (-1, -1),
            "se" => (1, -1),
            "sw" => (1, 1),
            _ => (0, 0)
        };
        return newPos;
    }

    private bool PointGetsCombat()
    {
        var r = new Random();
        var chance = r.Next(1, 11);
        return chance >= _pointInteractionThreshold;
    }
}