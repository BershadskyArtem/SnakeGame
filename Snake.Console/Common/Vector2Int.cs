namespace Snake.Console.Common;

public struct Vector2Int
{
    private bool Equals(Vector2Int other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is Vector2Int other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public int X;
    public int Y;

    public static Vector2Int operator +(Vector2Int one, Vector2Int other)
    {
        return new Vector2Int
        {
            X = one.X + other.X,
            Y = one.Y + other.Y
        };
    }

    public static Vector2Int operator -(Vector2Int one, Vector2Int other)
    {
        return new Vector2Int
        {
            X = one.X - other.X,
            Y = one.Y - other.Y
        };
    }

    public static bool operator ==(Vector2Int one, Vector2Int other)
    {
        return one.X == other.X && one.Y == other.Y;
    }

    public static bool operator !=(Vector2Int one, Vector2Int other)
    {
        return !(one == other);
    }

    public static Vector2Int InvertedUp => new()
    {
        Y = -1,
        X = 0
    };

    public static Vector2Int InvertedDown => new()
    {
        Y = 1,
        X = 0
    };

    public static Vector2Int Right => new()
    {
        X = 1,
        Y = 0
    };

    public static Vector2Int Left => new()
    {
        X = -1,
        Y = 0
    };


}