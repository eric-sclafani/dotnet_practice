namespace Game.Models;

public record GameArgs
{
    public int Rows { get; set; }
    public int Cols { get; set; }

    public override string ToString()
    {
        return $"Rows={Rows}\nCols={Cols}";
    }

    public void ValidateArgs()
    {
        if (Rows == 0 || Cols == 0)
            throw new ArgumentException("Rows or Cols cannot be zero");

        if (Cols % 2 == 0)
            throw new ArgumentException("Parameter 'Cols' must be odd to initialize tile map.");
    }
}