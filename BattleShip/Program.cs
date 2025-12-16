using BattleShip;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;
using System.Media;
using System.Runtime.CompilerServices;

namespace BattleShip
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Fullscreen
            var inputSimulator = new InputSimulator();
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.F11);

            Console.CursorVisible = false;
            Thread.Sleep(500);

            // Create core game objects ONCE
            var board = new Board();
            board.InitializeBoard();

            var design = new Design();

            var placement = new Placement(board);
            var pm = new Placement.PlacementManager(placement, design);

            var bot = new Bot();
            var battleLogic = new BattleLogic(board, bot);

            var gameFlow = new GameFlow(board, design, pm, battleLogic);

            // Play background music
            string Music = AppDomain.CurrentDomain.BaseDirectory + @"Assets\the-last-point-beat-electronic-digital-394291.wav"; //music file path
            SoundPlayer soundPlayer = new SoundPlayer();
            soundPlayer.SoundLocation = Music;
            soundPlayer.PlayLooping();

            //Show main menu
            design.boarder();
            design.MainMenu();
            design.MenuOptions();

            var key = Console.ReadKey(true);

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



                // --- PLAYER SHIP PLACEMENT ---
                for (int i = 0; i < 5; i++)
                {
                    inputSimulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.OEM_PLUS);
                }
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

                var inputStart = Console.ReadKey(true);

                if (inputStart.Key == ConsoleKey.Enter)
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
                else if (inputStart.Key == ConsoleKey.T)
                {
                    Console.CursorVisible = false;

                    Console.Clear();
                    design.Tutorial();
                    Console.ReadKey(true);
                    Console.Clear();
                    design.MainMenu();
                }
                else if (inputStart.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
