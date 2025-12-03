using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace BattleShip
{
    internal class Program
    {
        static void Main(string[] args)
        {

            InputSimulator inputSimulator = new InputSimulator();
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.F11);

            Console.CursorVisible = false;
            Thread.Sleep(500);

            Board Board = new Board();
            Design design = new Design();
            PlacementManager pm = new PlacementManager();
            BattleLogic battleLogic = new BattleLogic();
            

            // BOT SETUP
            Bot bot = new Bot();
            Board.InitializeBoard();
            bot.PlaceShips();

            // Copy real bot ships to the board object
            for (int r = 0; r < 10; r++)
                for (int c = 0; c < 10; c++)
                    Board.Hidden_BotBoard[r, c] = Board.Hidden_BotBoard[r, c];

            battleLogic.Bot = bot;

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

                    pm.PlaceAllShips(Board, ships);

                    Console.Clear();
                    Console.WriteLine("Ship placement finished! Press any key...");
                    Console.ReadKey(true);

                    Console.Clear();
                    Board.Draw(true);

                    Console.WriteLine("Press ENTER to start the match...");
                    var inputStart = Console.ReadKey(true);

                    if (inputStart.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();

                        design.header();
                        battleLogic.GameStart(Board);

                    }
                    else if (key.Key == ConsoleKey.T)
                    {
                        Console.Clear();
                        design.Tutorial();
                        Console.ReadKey(true);
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }

                    Console.Clear();
                }
            }
        }
    }
}
