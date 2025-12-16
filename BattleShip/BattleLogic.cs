using BattleShip;
using System;
using System.Threading;

namespace BattleShip
{
    public class BattleLogic
    {
        public Bot Bot { get; private set; }
        public Board Board { get; private set; }
        public int PlayerShips { get; private set; } = 5;
        public int BotShips { get; private set; } = 5;

        // Hunting state for an AI that needs it (this is optional if we want more advanced AI later)
        private int _lastHitRow = -1;
        private int _lastHitCol = -1;
        private bool _isHunting = true;

        public Design design = new Design();

        public BattleLogic(Board board, Bot bot, int playerShips = 5, int botShips = 5)
        {
            Board = board ?? throw new ArgumentNullException(nameof(board));
            Bot = bot ?? throw new ArgumentNullException(nameof(bot));
            PlayerShips = playerShips;
            BotShips = botShips;
        }

        // Simple input validator (A1..J10)
        public bool IsValidInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            input = input.Trim().ToUpperInvariant();

            if (input.Length < 2 || input.Length > 3) return false; // allow 1-10 -> length 2 or 3

            char col = input[0];
            if (col < 'A' || col > 'J') return false;

            if (!int.TryParse(input.Substring(1), out int row)) return false;
            if (row < 1 || row > 10) return false;

            return true;
        }

        // Update internal record of bot's display board and relay to Bot AI if needed (again, optional for future update :D )
        public void ProcessShotResult(int row, int col, char result, bool shipSunk)
        {
            if (!InBounds(row, col)) return;

            // result should be 'H' (hit) or 'M' (miss)
            Board.Display_BotBoard[row, col] = result;

            // Keep a basic hunting state if Bot needs it
            if (result == 'H')
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

            Bot?.ProcessShotResult(row, col, result, shipSunk);
        }

        // PLAYER TURN:
        public bool PlayerTurn(int row, int col)
        {
            if (!InBounds(row, col))
            {
                Console.SetCursorPosition(45, 30);
                Console.WriteLine("Coordinate out of bounds.");
                Thread.Sleep(1000);
                return false;
            }

            // if Already fired
            if (Board.Display_BotBoard[row, col] != '~')
            {
                Console.SetCursorPosition(40, 30);
                Console.WriteLine(": You already fired at this coordinate.");
                Thread.Sleep(1000);
                return false;
            }

            // Hit
            if (Board.Hidden_BotBoard[row, col] == 'S')
            {
                Board.Hidden_BotBoard[row, col] = 'H';
                Board.Display_BotBoard[row, col] = 'H';

                Console.SetCursorPosition(45, 30);
                Console.WriteLine(design.GREEN_BG + "HIT!" + design.RESET);
                Thread.Sleep(1000);

                bool allSunk = IsShipSunk(Board.Hidden_BotBoard);
                if (allSunk)
                {
                    BotShips = 0;

                    Console.SetCursorPosition(45, 30);
                    Console.WriteLine(design.GREEN_BG + "You sunk all bot ships!" + design.RESET);
                    Thread.Sleep(1000);
                }

                Bot?.ProcessShotResult(row, col, 'H', allSunk);
                return true;
            }

            // Miss
            Board.Display_BotBoard[row, col] = 'M';

            Console.SetCursorPosition(45, 30);
            Console.WriteLine(design.RED_BG + "MISS!" + design.RESET);
            Thread.Sleep(1000);
            Bot?.ProcessShotResult(row, col, 'M', false);
            return true;
        }

        // BOT TURN:
        public void BotTurn()
        {
            var move = Bot.MakeMove(Board);
            int r = move.Item1;
            int c = move.Item2;

            if (!InBounds(r, c)) return;

            if (Board.Hidden_PlayerBoard[r, c] == 'S')
            {
                Board.Hidden_PlayerBoard[r, c] = 'H';
                Board.Display_PlayerBoard[r, c] = 'H';

                bool allSunk = IsShipSunk(Board.Hidden_PlayerBoard);
                if (allSunk)
                {
                    PlayerShips = 0;
                    Console.SetCursorPosition(45, 30);
                    Console.WriteLine(design.GREEN_BG + "BOT sunk all your ships!" + design.RESET);
                    Thread.Sleep(1000);
                }

                // Bot should be notified of a hit
                Bot?.ProcessShotResult(r, c, 'H', allSunk);
                Console.SetCursorPosition(45, 30);
                Console.WriteLine(design.GREEN_BG + $"BOT hits at {r + 1},{c + 1}" + design.RESET);
                Thread.Sleep(1000);
            }
            else
            {
                // Miss - mark player's display if not already marked
                if (Board.Display_PlayerBoard[r, c] == '~')
                {
                    Board.Display_PlayerBoard[r, c] = 'M';
                }

                Bot?.ProcessShotResult(r, c, 'M', false);
                Console.SetCursorPosition(45, 30);
                Console.WriteLine(design.RED_BG + $"BOT misses at {r + 1},{c + 1}" + design.RESET);
                Thread.Sleep(1000);
            }
        }

        // Returns true when there are no 'S' left anywhere on that hidden grid
        public bool IsShipSunk(char[,] grid)
        {
            if (grid == null) return true;
            foreach (char ch in grid)
            {
                if (ch == 'S') return false;
            }
            return true;
        }

        private bool InBounds(int row, int col)
        {
            if (Board == null) return false;
            int size = Board.Size;
            return row >= 0 && row < size && col >= 0 && col < size;
        }
    }
}
