using System;
using System.Collections.Generic;

namespace BattleShip
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Board playerBoard = new Board();
            PlacementManager pm = new PlacementManager();

            while (true)
            {
                playerBoard.MainMenu();
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();

// ============================Ships na for placement Stage============================
                    var ships = new List<Ship>()
                    {
                        new Ship("Destroyer", 2, Orientation.Horizontal),
                        new Ship("Submarine", 3, Orientation.Horizontal),
                        new Ship("Cruiser", 3, Orientation.Horizontal),
                        new Ship("Battleship", 4, Orientation.Horizontal),
                        new Ship("Carrier", 5, Orientation.Horizontal)
                    };

// ============================All stored ships and boardInfo here after placement============================
                    pm.PlaceAllShips(playerBoard, ships);

                    Console.Clear();
                    Console.WriteLine("Ship placement finished!");
                    Console.WriteLine("Press any key to show your board...");
                    Console.ReadKey(true);

// ============================Over view ng board after all ships are placed==================================
                    Console.Clear();
                    playerBoard.Draw(true);
                    Console.WriteLine("\nPress Esc to return to menu.");
                    Console.ReadKey(true);
                }
                else if (key.Key == ConsoleKey.T)
                {
                    Console.Clear();
                    playerBoard.Tutorial();
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
