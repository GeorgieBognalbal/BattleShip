using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;

namespace BattleShip
{
    public class GameStart
    {

        public void Start()
        {

            // Create core game objects ONCE
            var inputSimulator = new InputSimulator();

            var board = new Board();
            board.InitializeBoard();
            var design = new Design();

            var placement = new Placement(board);
            var pm = new Placement.PlacementManager(placement, design);

            var bot = new Bot();
            var gameFlow = new GameFlow(board, design, pm, new BattleLogic(board, bot));


            //Show main menu
            design.MainMenu();

            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.Enter)
            {
                Console.Clear();

                // List of ships to place
                var ships = new List<Ship>()
                {
                new Ship("Destroyer", 2, Orientation.Horizontal),
                new Ship("Submarine", 3, Orientation.Horizontal),
                new Ship("Cruiser", 3, Orientation.Horizontal),
                new Ship("Battleship", 4, Orientation.Horizontal),
                new Ship("Carrier", 5, Orientation.Horizontal)
                };


                for (int i = 0; i < 5; i++)
                {
                    inputSimulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.OEM_PLUS);
                }

                // --- PLAYER SHIP PLACEMENT ---
                pm.PlaceAllShips(board, ships);

                // --- BOT SHIP PLACEMENT ---
                bot.PlaceShips(board);

                // Sync bot's ships to board.Hidden_BotBoard
                for (int r = 0; r < 10; r++)
                {
                    for (int c = 0; c < 10; c++)
                    {
                        board.Hidden_BotBoard[r, c] = bot.Hidden_BotBoard[r, c];
                    }
                }

                Console.CursorVisible = false;
                Console.Clear();
                Thread.Sleep(10);
                design.OverviewBoader();
                board.Draw(true);

                var inputStart = Console.ReadKey().Key;

                if (inputStart == ConsoleKey.Enter)
                {
                    Console.CursorVisible = false;
                    Console.Clear();
                    Console.SetCursorPosition(4, 10);
                    design.BotPlacing();
                    Thread.Sleep(2000);

                    Console.Clear();

                    Console.SetCursorPosition(5, 10);
                    design.GameStart_Header();
                    Thread.Sleep(2000);

                    gameFlow.GameStart();
                    return;   // stops code from falling through to main menu

                }

            }

            
            else if (key.Key == ConsoleKey.T)
            {
                Console.Clear();
                for (int i = 0; i < 5; i++)
                {
                    inputSimulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.OEM_PLUS);
                }
                Thread.Sleep(20);
                design.Tutorial();
            }

            var key_return = Console.ReadKey(true);

            if (key_return.Key == ConsoleKey.X)
            {
                Console.Clear();
                for (int i = 0; i < 5; i++)
                {
                    inputSimulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.OEM_MINUS);
                }
                Start();
            }
            else if (key_return.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }

            if (key.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }
    }
}
