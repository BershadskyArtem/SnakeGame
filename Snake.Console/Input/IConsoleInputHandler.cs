namespace Snake.Console.Input;

public interface IConsoleInputHandler : IInputHandler
{
    /// <summary>
    /// Action on key pressed. 1st arg is a new key and the 2nd is the previous one. DOES NOT FIRE ON DUPLICATE KEYS
    /// </summary>
    public event Action<ConsoleKeyInfo, ConsoleKeyInfo> OnKeyPressed;
}