using BattleShip;
using System;
using System.Threading;
using static BattleShip_v2.Placement;

namespace BattleShip_v2
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

            while (true)
            {
                Console.Clear();
                _design.header();
                Board.BoardDisplay.ShowSideBySide(_board);

                Console.Write("\nEnter attack coordinate (ex: B5): ");
                string input = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

                // Use BattleLogic's validator (PlacementManager is ONLY for placing)
                if (!_battleLogic.IsValidInput(input))
                {
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
                    Console.WriteLine("Try again.");
                    Thread.Sleep(1000);
                    continue; // DO NOT let bot fire
                }

                // Check win for player
                if (_battleLogic.BotShips == 0)
                {
                    Console.Clear();
                    Board.BoardDisplay.ShowSideBySide(_board);
                    Console.WriteLine("\nPLAYER WIN!");
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
                    Console.WriteLine("\nBOT WINS!");
                    return;
                }
            }
        }
    }
}
