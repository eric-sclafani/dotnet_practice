using Game.Modules.PlayerInteraction;

namespace Game.Modules.World;

public class World
{
    private readonly int _rows;
    private readonly int _cols;
    private readonly Point[,] _grid;
    private Point _playerPosition { get; set; }

    public World(int rows, int cols)
    {
        _rows = rows;
        _cols = cols;
        _grid = InitGrid();
        _playerPosition = SetPlayerStartPosition();
    }

    public bool ChangePlayerPosition(string? playerInput)
    {
        var lastPosition = _playerPosition;
        var newPos = GetNewCoord(playerInput);

        try
        {
            var newX = _playerPosition.X + newPos.x;
            var newY = _playerPosition.Y + newPos.y;
            _playerPosition = _grid[newX, newY];
            Console.WriteLine($"You moved from {lastPosition} to to {_playerPosition}");
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
        for (var row = 0; row < _rows; row++)
        {
            for (var col = 0; col < _cols; col++)
            {
                var point = _grid[row, col];

                if (point == _playerPosition)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
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

    private static bool PointGetsInteraction()
    {
        var r = new Random();

    }
    
}