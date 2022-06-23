namespace Utils;

public static class Navigation
{
    /// <summary>
    /// Given a starting <see cref="Point2D"/> and a directional symbol of '^' (north), 'v' (south), '>' (east), or '<' (west)
    /// returns a new <see cref="Point2D"/> representing the next delivery location.
    /// </summary>
    public static Point2D NextDeliveryLocation(this Point2D origin, char direction) => direction switch
    {
        '^' => origin with { X = origin.X, Y = origin.Y + 1 },
        'v' => origin with { X = origin.X, Y = origin.Y - 1 },
        '>' => origin with { X = origin.X + 1, Y = origin.Y },
        '<' => origin with { X = origin.X - 1, Y = origin.Y },
        _ => throw new ArgumentException("Unknown direction symbol")
    };
}