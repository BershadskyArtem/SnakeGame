using Snake.Console;
using Snake.Console.Display;
using Snake.Console.Input;

/*
 * This demo is not supposed to represent my work skills since i do not put to much effort in here.  
 * 
 * This game uses ECS system approach (It means that data and logic are separated)
 * See Entitas library for Unity on github https://github.com/sschmid/Entitas
 * and this https://unity.com/ecs
 */

Console.CursorVisible = false;
var game = new Game(
    new ConsoleInputHandler(), 
    new ConsoleDisplay());

game.Run();
