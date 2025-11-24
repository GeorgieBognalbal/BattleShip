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
            InputSimulator inputSimulator = new InputSimulator();
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.F11);

            Console.CursorVisible = false;

            Thread.Sleep(500);
            Board playerBoard = new Board();
            Design design = new Design();
            PlacementManager pm = new PlacementManager();
            BattleLogic battleLogic = new BattleLogic();

            design.MainMenu();
            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.ADD);
                    inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.ADD);
                    Thread.Sleep(200);
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
                    Console.WriteLine("Press ENTER to Start match...");
                    var inputStart = Console.ReadKey(true);

//============================Start of Game===================================================================
                    if (inputStart.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();

                        battleLogic.Player1(playerBoard);


                    }   
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
