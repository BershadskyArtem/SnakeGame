namespace Snake.Console.Input;

public class ConsoleInputHandler : IConsoleInputHandler
{
    public event Action<ConsoleKeyInfo, ConsoleKeyInfo>? OnKeyPressed;
    public event Action? OnExitRequested;
    public ConsoleKeyInfo KeyInfo { get; set; }
    public ConsoleKeyInfo PreviousKeyInfo { get; set; }
    /// <summary>
    /// SHIT DOES NOT NEED TO BE HERE. THIS CAN BE DONE JUST FINE WITHOUT EVENTS
    /// </summary>
    private bool _firstTimeFix = true;
    
    public ConsoleInputHandler()
    {
        KeyInfo = new ConsoleKeyInfo('d', ConsoleKey.D, false, false, false);
        PreviousKeyInfo = new ConsoleKeyInfo();
    }

    public void PollEvents()
    {
        if (!System.Console.KeyAvailable && !_firstTimeFix) return;
        
        if (_firstTimeFix)
        {
            _firstTimeFix = false;
            OnKeyPressed?.Invoke(KeyInfo, PreviousKeyInfo);
            return;    
        }
        
        var newKeyInfo = System.Console.ReadKey(true);
        
        if(newKeyInfo == KeyInfo)
            return;
        
        PreviousKeyInfo = KeyInfo;

        KeyInfo = newKeyInfo;

        if (KeyInfo.Key == ConsoleKey.Escape)
            OnExitRequested?.Invoke();
        else
            OnKeyPressed?.Invoke(KeyInfo, PreviousKeyInfo);
    }
}