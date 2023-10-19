using Snake.Console.Common;
using Snake.Console.Display;
using Snake.Console.Input;

namespace Snake.Console;

public class Game
{
    private const int Height = 20;
    private const int Width = 30;

    private readonly Vector2Int[] _snake;
    private readonly IConsoleInputHandler _inputHandler;
    private readonly IConsoleDisplay _consoleDisplay;

    private ConsoleKeyInfo _keyInfo = new('d', ConsoleKey.D, false, false, false);
    private ConsoleKeyInfo _previousKeyInfo;
    private Vector2Int _fruit;
    private int _parts = 3;
    private int _score;


    public Game(IConsoleInputHandler inputHandler, IConsoleDisplay consoleDisplay)
    {
        _inputHandler = inputHandler;
        _consoleDisplay = consoleDisplay;

        _inputHandler.OnKeyPressed += (created, previous) =>
        {
            _keyInfo = created;
            _previousKeyInfo = previous;
        };

        _snake = new Vector2Int[50];

        InitSnake();

        SpawnNewFruit();
    }

    /// <summary>
    /// As title says. Runs the main game loop.
    /// </summary>
    public void Run()
    {
        while (true)
        {
            _consoleDisplay.DrawBorder(Width, Height);
            Tick();
            _consoleDisplay.DrawScore(Width, Height + 4, _score);
            Thread.Sleep(150);
        }
        // ReSharper disable once FunctionNeverReturns
    }


    private void InitSnake()
    {
        _snake[0] = new Vector2Int
        {
            X = 5,
            Y = 5
        };

        _snake[1] = new Vector2Int
        {
            X = 5,
            Y = 6
        };

        _snake[2] = new Vector2Int
        {
            X = 5,
            Y = 7
        };
    }

    private void SpawnNewFruit()
    {
        var cell = new Vector2Int
        {
            X = Random.Shared.Next(2, Width - 2),
            Y = Random.Shared.Next(2, Height - 2)
        };

        var stupidLock = 0;

        while (_snake.Contains(cell) && stupidLock < 100)
        {
            stupidLock++;
            cell = new Vector2Int
            {
                X = Random.Shared.Next(2, Width - 2),
                Y = Random.Shared.Next(2, Height - 2)
            };
        }

        _fruit = cell;
    }


    /// <summary>
    /// This monster is created for you. EVENTS ARE NOT SUITED FOR GAMES AT THIS SCALE.
    /// </summary>
    /// <param name="keyInfo"></param>
    /// <param name="previousKeyInfo"></param>
    private void HandleKeyPressed(ConsoleKeyInfo keyInfo, ConsoleKeyInfo previousKeyInfo)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.W:
            {
                if (previousKeyInfo.Key == ConsoleKey.S)
                {
                    _snake[0] += Vector2Int.InvertedDown;
                }
                else
                    _snake[0] += Vector2Int.InvertedUp;

                return;
            }
            case ConsoleKey.A:
            {
                if (previousKeyInfo.Key == ConsoleKey.D)
                {
                    _snake[0] += Vector2Int.Right;
                }
                else
                    _snake[0] += Vector2Int.Left;

                return;
            }
            case ConsoleKey.S:
            {
                if (previousKeyInfo.Key == ConsoleKey.W)
                {
                    _snake[0] += Vector2Int.InvertedUp;
                }
                else
                    _snake[0] += Vector2Int.InvertedDown;

                return;
            }
            case ConsoleKey.D:
            {
                if (previousKeyInfo.Key == ConsoleKey.A)
                {
                    _snake[0] += Vector2Int.Left;
                }
                else
                    _snake[0] += Vector2Int.Right;

                return;
            }
            default:
                return;
        }
    }

    /// <summary>
    /// Main game logic
    /// </summary>
    private void Tick()
    {
        //Move snake
        for (int i = _parts; i > 1; i--)
        {
            _snake[i - 1] = _snake[i - 2];
        }

        //POLL EVENTS
        //THIS SHIT IS DONE ON PURPOSE
        //NO NEED TO TELL ME THAT THIS IS OVERENGINEERING
        _inputHandler.PollEvents();

        //We need to move whether or not gamer pressed the button.
        HandleKeyPressed(_keyInfo, _previousKeyInfo);

        //If head touches the body
        if (_snake[0] == _fruit)
        {
            _parts++;
            _score += 100;
            SpawnNewFruit();
        }

        //Draw each body part
        for (int i = 0; i < _parts; i++)
        {
            _consoleDisplay.DrawSnakeSegment(_snake[i]);
        }

        //Draw fruit
        _consoleDisplay.DrawFruit(_fruit);

        //Checks if collision with itself or with level happened
        var didHit = CheckCollisions();

        if (didHit)
        {
            System.Console.Clear();
            System.Console.WriteLine($"Game Over: {_score}");
            System.Console.WriteLine("Press any button to exit.");
            System.Console.ReadKey();
            Environment.Exit(0);
        }
    }

    private bool CheckCollisions()
    {
        //Borders check
        bool didHit = _snake[0].X > Width || _snake[0].X <= 1 || _snake[0].Y <= 1 || _snake[0].Y - 2 >= Height;

        //If any segments have the same coordinates it means that collision happened.
        didHit = didHit |
                 _snake
                     .Take(_parts)
                     .GroupBy(x => x)
                     .Where(g => g.Count() > 1)
                     .Select(g => g.Key)
                     .Any();

        return didHit;
    }
}