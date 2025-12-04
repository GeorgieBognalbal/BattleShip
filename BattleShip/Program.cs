using System;
using System.Collections.Generic;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace BattleShip
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var inputSimulator = new InputSimulator();
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.F11);

            Console.CursorVisible = false;
            Thread.Sleep(500);

            // Create game objects
            var playerBoard = new Board();
            var design = new Design();
            var pm = new PlacementManager();
            var battleLogic = new BattleLogic();
            var bot = new Bot();

            // Initialize boards and place bot ships
            playerBoard.InitializeBoard();
            bot.PlaceShips();

            // Provide bot and player board references to battle logic
            battleLogic.Bot = bot;
            battleLogic.PlayerBoard = playerBoard;

            // Show main menu
            design.MainMenu();

            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();

                    var ships = new List<Ship>()
                    {
                        new Ship("Destroyer", 2, Orientation.Horizontal),
                        new Ship("Submarine", 3, Orientation.Horizontal),
                        new Ship("Cruiser", 3, Orientation.Horizontal),
                        new Ship("Battleship", 4, Orientation.Horizontal),
                        new Ship("Carrier", 5, Orientation.Horizontal)
                    };

                    pm.PlaceAllShips(playerBoard, ships);

                    Console.Clear();
                    Console.WriteLine("Ship placement finished! Press any key...");
                    Console.ReadKey(true);

                    Console.Clear();
                    playerBoard.Draw(true);

                    Console.WriteLine("Press ENTER to start the match...");
                    var inputStart = Console.ReadKey(true);

                    if (inputStart.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        design.header();
                        battleLogic.GameStart(playerBoard);
                    }
                    else if (inputStart.Key == ConsoleKey.T)
                    {
                        Console.Clear();
                        design.Tutorial();
                        Console.ReadKey(true);
                    }
                    else if (inputStart.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }

                    Console.Clear();
                }
            }
        }
    }
}
