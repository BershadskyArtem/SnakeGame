using Snake.Console.Common;

namespace Snake.Console.Display;

public interface IConsoleDisplay
{
    public void DrawSnakeSegment(Vector2Int vector);
    public void DrawBorder(int width, int height);
    public void DrawFruit(Vector2Int fruit);
    public void DrawScore(int x, int y, int score);
}