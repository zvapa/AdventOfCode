namespace Utils;

public record Point2D(int X, int Y);

public readonly record struct Point2D_rrs(int X, int Y);

public readonly struct Point2D_rs
{
    public readonly int X;
    public readonly int Y;

    public Point2D_rs(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public readonly record struct Matrix<T>(int Rows, int Columns) : IEnumerable<T>
{
    private readonly T[,] _grid = new T[Rows, Columns];

    public T this[int row, int column]
    {
        get => _grid[row, column];
        set => _grid[row, column] = value;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                yield return _grid[i, j];
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
