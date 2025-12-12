using BattleShip;
using System;
using System.Threading;

namespace BattleShip
{
    public class BattleLogic
    {
        // Single authoritative references to the Board and Bot instances
        public Bot Bot { get; private set; }
        public Board Board { get; private set; }
        public int PlayerShips { get; private set; } = 5;
        public int BotShips { get; private set; } = 5;

        // Hunting state for an AI that needs it (kept for potential Bot/AI usage)
        private int _lastHitRow = -1;
        private int _lastHitCol = -1;
        private bool _isHunting = true;

        // Constructor — pass the actual instances created during setup
        public BattleLogic(Board board, Bot bot, int playerShips = 5, int botShips = 5)
        {
            Board = board ?? throw new ArgumentNullException(nameof(board));
            Bot = bot ?? throw new ArgumentNullException(nameof(bot));
            PlayerShips = playerShips;
            BotShips = botShips;
        }

        // Sync the bot display board with whatever representation Board has
        // NOTE: Do NOT re-initialize the board here; initialization should happen once during game setup.

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

        // Update internal record of bot's display board and relay to Bot AI if needed
        public void ProcessShotResult(int row, int col, char result, bool shipSunk)
        {
            // Defensive bounds
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

            // If Bot has a method to learn from results, pass it along
            Bot?.ProcessShotResult(row, col, result, shipSunk);
        }

        // PLAYER TURN: player shoots at (row, col) — zero-based indices expected
        public bool PlayerTurn(int row, int col)
        {
            if (!InBounds(row, col))
            {
                Console.WriteLine("Coordinate out of bounds.");
                Thread.Sleep(500);
                return false;
            }

            // Already fired?
            if (Board.Display_BotBoard[row, col] != '~')
            {
                Console.WriteLine("You already fired at this coordinate.");
                Thread.Sleep(500);
                return false;
            }

            // Hit
            if (Board.Hidden_BotBoard[row, col] == 'S')
            {
                Board.Hidden_BotBoard[row, col] = 'H';
                Board.Display_BotBoard[row, col] = 'H';
                Console.WriteLine("HIT!");
                Thread.Sleep(500);

                bool allSunk = IsShipSunk(Board.Hidden_BotBoard);
                if (allSunk)
                {
                    BotShips = 0;
                    Console.WriteLine("You sunk all bot ships!");
                }

                Bot?.ProcessShotResult(row, col, 'H', allSunk);
                return true;
            }

            // Miss
            Board.Display_BotBoard[row, col] = 'M';
            Console.WriteLine("MISS!");
            Thread.Sleep(500);
            Bot?.ProcessShotResult(row, col, 'M', false);
            return true;
        }

        // BOT TURN: Bot picks coordinates and acts on player's board
        public void BotTurn()
        {
            // Ask Bot to make a move using the player's board as context.
            // IMPORTANT: Make sure Bot.MakeMove expects a Board instance and returns a tuple (int row, int col).
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
                    Console.WriteLine("BOT sunk all your ships!");
                    Thread.Sleep(500);
                }

                // Bot should be notified of a hit
                Bot?.ProcessShotResult(r, c, 'H', allSunk);
                Console.WriteLine($"BOT hits at {r + 1},{c + 1}");
                Thread.Sleep(500);
            }
            else
            {
                // Miss - mark player's display if not already marked
                if (Board.Display_PlayerBoard[r, c] == '~')
                {
                    Board.Display_PlayerBoard[r, c] = 'M';
                }

                Bot?.ProcessShotResult(r, c, 'M', false);
                Console.WriteLine($"BOT misses at {r + 1},{c + 1}");
                Thread.Sleep(500);
            }
        }

        // Returns true when there are no 'S' left anywhere on that hidden grid
        public bool IsShipSunk(char[,] grid)
        {
            if (grid == null) return true; // defensively treat null as "no ships"
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
