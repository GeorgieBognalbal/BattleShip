using BattleShip;
using System;
using System.Threading;
using static BattleShip.Placement;
using WindowsInput;

namespace BattleShip
{
    public class GameFlow
    {
        private readonly Board _board;
        private readonly Design _design;
        private readonly PlacementManager _placementManager;
        private readonly BattleLogic _battleLogic;

        public GameFlow(Board board, Design design, PlacementManager placementManager, BattleLogic battleLogic)
        {
            _board = board ?? throw new ArgumentNullException(nameof(board));
            _design = design ?? throw new ArgumentNullException(nameof(design));
            _placementManager = placementManager ?? throw new ArgumentNullException(nameof(placementManager));
            _battleLogic = battleLogic ?? throw new ArgumentNullException(nameof(battleLogic));
        }

        public void GameStart()
        {
            InputSimulator inputSimulator = new InputSimulator();

            inputSimulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.CONTROL, WindowsInput.Native.VirtualKeyCode.OEM_MINUS);

            while (true)
            {

                Console.Clear();
                _design.battleBanner();
                Board.BoardDisplay.ShowSideBySide(_board);                
                _design.BattleBoader();

                Console.SetCursorPosition(37, 29);
                Console.Write("Enter attack coordinate (ex: B5)");

                Console.SetCursorPosition(37, 30);
                string input = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

                if (!_battleLogic.IsValidInput(input))
                {
                    Console.SetCursorPosition(45, 30);
                    Console.WriteLine("Invalid input! Try again.");
                    Thread.Sleep(1000);
                    continue;
                }

                int col = input[0] - 'A';
                int row = int.Parse(input.Substring(1)) - 1;

                // Player turn
                bool valid = _battleLogic.PlayerTurn(row, col);
                if (!valid)
                {
                    Console.SetCursorPosition(45, 30);
                    Console.WriteLine("Try again.");
                    Thread.Sleep(1000);
                    continue; // DO NOT let bot fire
                }

                // Check win for player
                if (_battleLogic.BotShips == 0)
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 10);
                    _design.playerWon();
                    Console.ReadKey(true);
                    return;
                }

                // Bot turn
                _battleLogic.BotTurn();

                // Display boards after bot move
                Console.Clear();
                Board.BoardDisplay.ShowSideBySide(_board);

                // Check win for bot
                if (_battleLogic.IsShipSunk(_board.Hidden_PlayerBoard))
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 10);
                    _design.botWon();
                    return;
                }
            }
        }
    }
}
