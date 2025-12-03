using System;
using System.Diagnostics.Eventing.Reader;

namespace BattleShip
{
    internal class BattleLogic
    {
        public Bot Bot;
        public Board Board;

        private int playerShips = 5;
        private int botShips = 5;

        private void SyncBotDisplay()
        {
            Board.SyncBotBoard();
        }

        public void GameStart(Board board)
        {
            Design design = new Design();
            Board = board;

            Console.Clear();

            design.header();
            Board.BoardDisplay.ShowSideBySide(board);

            while (true)
            {
                Console.WriteLine("\nEnter attack coordinate (ex: B5): ");

                string input = Console.ReadLine().ToUpper();

                if (!IsValidInput(input))
                {
                    Console.WriteLine("Invalid input!");
                    Console.Clear();
                    continue;
                }

                int col = input[0] - 'A';
                int row = int.Parse(input.Substring(1)) - 1;

                PlayerTurn(row, col);
                if (botShips == 0)
                {
                    Console.Clear();
                    Console.WriteLine("\nYOU WIN!");
                    return;
                }

                if (playerShips == 0)
                {
                    Console.Clear();
                    Console.WriteLine("\nBOT WINS!");
                    return;
                }
                else 
                {
                    Console.Clear();
                    Board.BoardDisplay.ShowSideBySide(board);
                    continue;
                }

                
            }

        }

        private void BotTurn(Board playerBoard)
        {
            var (row, col, result) = Bot.MakeMove();

            if (result == 'O')
            {
                Console.WriteLine($"BOT hits at {row + 1},{col + 1}");
            }
            else
            {
                Console.WriteLine($"BOT misses at {row + 1},{col + 1}");
            }
        }

        private bool IsValidInput(string input)
        {
            if (input.Length < 2) return false;
            if (input[0] < 'A' || input[0] > 'J') return false;

            if (!int.TryParse(input.Substring(1), out int r)) return false;
            if (r < 1 || r > 10) return false;

            return true;
        }

        public int _lastHitRow = -1;
        public int _lastHitCol = -1;
        public bool _isHunting = true;

        public void ProcessShotResult(int row, int col, char result, bool shipSunk)
        {

            Board.Display_BotBoard[row, col] = result;

            if (result == 'X')
            {
                if (shipSunk)
                {
                    _isHunting = true;
                    _lastHitRow = -1;
                    _lastHitCol = -1;
                }
                else
                {
                    _isHunting = false;
                    _lastHitRow = row;
                    _lastHitCol = col;
                }
            }
        }

        //========================= PLAYER TURN =========================
        private void PlayerTurn(int row, int col)
        {
            while (true)
            {
                if (Board.Hidden_BotBoard[row, col] == 'S')
                {
                    Board.Hidden_BotBoard[row, col] = 'X';
                    Board.Display_BotBoard[row, col] = 'X';

                    Console.WriteLine("HIT!");
                    continue;
                }
                else if (IsShipSunk(Board.Hidden_BotBoard))
                {
                    botShips--;
                    Console.WriteLine("You sunk a ship!");
                    ProcessShotResult(row, col, 'H', true);
                    continue;
                }
                else
                {
                    ProcessShotResult(row, col, 'H', false);
                }
                if (Board.Display_BotBoard[row, col] == '~')
                {
                    Board.Display_BotBoard[row, col] = 'O';
                    Console.WriteLine("MISS!");
                    ProcessShotResult(row, col, 'M', false);
                }

                SyncBotDisplay();
            }
        }

        private bool IsShipSunk(char[,] grid)
        {
            foreach (char c in grid)
                if (c == 'S')
                    return false;
            return true;
        }
    }
}
