using Snake.Console.Common;

namespace Snake.Console.Display;

public class ConsoleDisplay : IConsoleDisplay
{
    public void DrawSnakeSegment(Vector2Int vector)
    {
        System.Console.SetCursorPosition(vector.X, vector.Y);
        System.Console.Write('#');
    }

    public void DrawBorder(int width, int height)
    {
        System.Console.Clear();

        for (var i = 1; i <= width + 2; i++)
        {
            System.Console.SetCursorPosition(i, 1);
            System.Console.Write("-");
        }

        for (var i = 1; i <= width + 2; i++)
        {
            System.Console.SetCursorPosition(i, height + 2);
            System.Console.Write("-");
        }

        for (var i = 1; i <= height + 1; i++)
        {
            System.Console.SetCursorPosition(1, i);
            System.Console.Write("|");
        }

        for (var i = 1; i <= height + 1; i++)
        {
            System.Console.SetCursorPosition(width + 2, i);
            System.Console.Write("|");
        }
    }

    public void DrawFruit(Vector2Int fruit)
    {
        DrawSnakeSegment(fruit);
    }

    public void DrawScore(int x, int y, int score)
    {
        System.Console.SetCursorPosition(x, y);
        System.Console.WriteLine($"Score: {score}");
    }
}