namespace Utils;

public static class Navigation
{
    /// <summary>
    /// Given a starting <see cref="Point2D"/> and a directional symbol of '^' (north), 'v' (south), '&gt;' (east),
    /// or '&lt;' (west) returns a new <see cref="Point2D"/> representing the next location.
    /// </summary>
    public static Point2D NextLocation(this Point2D origin, char direction) => direction switch
    {
        '^' => origin with { X = origin.X, Y = origin.Y + 1 },
        'v' => origin with { X = origin.X, Y = origin.Y - 1 },
        '>' => origin with { X = origin.X + 1, Y = origin.Y },
        '<' => origin with { X = origin.X - 1, Y = origin.Y },
        _ => throw new ArgumentException("Unknown direction symbol")
    };

    /// <summary>
    /// Given a starting <see cref="Point2D_struct"/> and a directional symbol of '^' (north), 'v' (south), '&gt;' (east),
    /// or '&lt;' (west) returns a new <see cref="Point2D_struct"/> representing the next location.
    /// </summary>
    public static Point2D_struct NextLocation(this Point2D_struct origin, char direction) => direction switch
    {
        '^' => new Point2D_struct(x: origin.X, y: origin.Y + 1),
        'v' => new Point2D_struct(x: origin.X, y: origin.Y - 1),
        '>' => new Point2D_struct(x: origin.X + 1, y: origin.Y),
        '<' => new Point2D_struct(x: origin.X - 1, y: origin.Y),
        _ => throw new ArgumentException("Unknown direction symbol")
    };
}