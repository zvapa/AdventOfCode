namespace Utils;

public record Point2D(int X, int Y);

public readonly struct Point2D_struct
{
    public readonly int X;
    public readonly int Y;

    public Point2D_struct(int x, int y)
    {
        X = x;
        Y = y;
    }
}