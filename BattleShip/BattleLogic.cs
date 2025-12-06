using System;

namespace BattleShip
{
    public class BattleLogic
    {
        public Bot Bot;
        public Board Board;

        public int playerShips = 5;
        public int botShips = 5;

        private void SyncBotDisplay()
        {
            // synchronize using the board's Display_BotBoard array
            Board.SyncBotBoard(Board.Display_BotBoard);
        }

        //=====================================GAME START===========================================
        public void GameStart(Board board)
        {
            Design design = new Design();

            // Set the active board reference(s)
            Board = board;

            Console.Clear();
            design.header();
            Board.BoardDisplay.ShowSideBySide(board);

            while (true)
            {
                Console.WriteLine("\nEnter attack coordinate (ex: B5): ");

                string input = Console.ReadLine()?.ToUpper() ?? string.Empty;

                if (!IsValidInput(input))
                {
                    Console.WriteLine("Invalid input!");
                    Console.Clear();
                    continue;
                }

                int col = input[0] - 'A';
                int row = int.Parse(input.Substring(1)) - 1;

                PlayerTurn(row, col);

                // Check win condition for bot
                if (IsShipSunk(Board.Hidden_BotBoard))
                {
                    Console.Clear();
                    Console.WriteLine("\nPLAYER WIN!");
                    return;
                }

                BotTurn();
                Board.BoardDisplay.ShowSideBySide(Board);

                // Check win condition for player
                if (IsShipSunk(Board.Hidden_PlayerBoard))
                {
                    Console.Clear();
                    Console.WriteLine("\nBOT WINS!");
                    return;
                }
                else
                {
                    Console.Clear();
                    GameStart(board);
                    Board.BoardDisplay.ShowSideBySide(board);
                    continue;
                }
            }
        }

        private bool IsValidInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
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
            // update bot display board with the result (H = hit, M = miss)
            Board.Display_BotBoard[row, col] = result;

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
        }

        //========================= PLAYER TURN =========================
        private void PlayerTurn(int row, int col)
        {
            // bounds check
            if (row < 0 || row >= Board.Size || col < 0 || col >= Board.Size)
            {
                Console.WriteLine("Coordinate out of bounds.");
                return;
            }

            // If there's a ship in the bot's hidden board -> hit
            if (Board.Hidden_BotBoard[row, col] == 'S')
            {
                Board.Hidden_BotBoard[row, col] = 'X';      // mark hidden as hit
                Board.Display_BotBoard[row, col] = 'X';     // mark display as hit
                Console.WriteLine("HIT!");

                bool sunk = IsShipSunk(Board.Hidden_BotBoard);
                if (sunk)
                {
                    botShips = 0; // all bot ships sunk
                    Console.WriteLine("You sunk all bot ships!");
                }

                // let the Bot AI know result (H = hit)
                Bot.ProcessShotResult(row, col, 'O', sunk);
            }
            else
            {
                // Miss case: if not already revealed
                if (Board.Display_BotBoard[row, col] == '~')
                {
                    Board.Display_BotBoard[row, col] = 'X';
                    Console.WriteLine("MISS!");
                    Bot.ProcessShotResult(row, col, 'X', false);
                }
                else
                {
                    Console.WriteLine("You already fired at this coordinate.");
                }
            }

            SyncBotDisplay();
        }

        //========================= BOT TURN =============================
        public void BotTurn()
        {
            // Bot needs the player's board to decide; Bot.MakeMove(Board) returns (int row, int col)
            (int r, int c) = Bot.MakeMove(Board);

            // bounds check
            if (r < 0 || r >= Board.Size || c < 0 || c >= Board.Size)
                return;

            // If bot hits player's ship
            if (Board.Hidden_PlayerBoard[r, c] == 'S')
            {
                Board.Hidden_PlayerBoard[r, c] = 'O';
                Board.Display_PlayerBoard[r, c] = 'O';

                bool sunk = IsShipSunk(Board.Hidden_PlayerBoard);
                if (sunk)
                {
                    playerShips = 0; // all player ships sunk
                    Bot.ProcessShotResult(r, c, 'O', true);
                }
                else
                {
                    Bot.ProcessShotResult(r, c, 'O', false);
                }

                Console.WriteLine($"BOT hits at {r + 1},{c + 1}");
            }
            else
            {
                // miss - mark player's display
                if (Board.Display_PlayerBoard[r, c] == '~')
                {
                    Board.Display_PlayerBoard[r, c] = 'X';
                }

                Bot.ProcessShotResult(r, c, 'X', false);
                Console.WriteLine($"BOT misses at {r + 1},{c + 1}");
            }
        }

        private bool IsShipSunk(char[,] grid)
        {
            foreach (char ch in grid)
                if (ch == 'S')
                    return false;
            return true;
        }
    }
}
