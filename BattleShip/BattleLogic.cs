using System;
using System.Diagnostics.Eventing.Reader;

namespace BattleShip
{
    internal class BattleLogic
    {
        public Bot Bot;
        public Board PlayerBoard;

        private int playerShips = 5;
        private int botShips = 5;

        private void SyncBotDisplay()
        {
            PlayerBoard.SyncBotBoard(Bot.display_BotBoard);
        }

        public void GameStart(Board board)
        {
            Design design = new Design();
            PlayerBoard = board;

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

                BotTurn();
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

        private bool IsValidInput(string input)
        {
            if (input.Length < 2) return false;
            if (input[0] < 'A' || input[0] > 'J') return false;

            if (!int.TryParse(input.Substring(1), out int r)) return false;
            if (r < 1 || r > 10) return false;

            return true;
        }

        //========================= PLAYER TURN =========================
        private void PlayerTurn(int row, int col)
        {
            while (true)
            {
                if (Bot.hidden_BotBoard[row, col] == 'S')
                {
                    Bot.hidden_BotBoard[row, col] = 'X';
                    Bot.display_BotBoard[row, col] = 'X';

                    Console.WriteLine("HIT!");
                    continue;
                }
                else if (IsShipSunk(Bot.hidden_BotBoard))
                {
                    botShips--;
                    Console.WriteLine("You sunk a ship!");
                    Bot.ProcessShotResult(row, col, 'H', true);
                    continue;
                }
                else
                {
                    Bot.ProcessShotResult(row, col, 'H', false);
                }
                if (Bot.display_BotBoard[row, col] == '~')
                {
                    Bot.display_BotBoard[row, col] = 'O';
                    Console.WriteLine("MISS!");
                    Bot.ProcessShotResult(row, col, 'M', false);
                }

                SyncBotDisplay();
            }
        }

        //========================= BOT TURN =============================
        public void BotTurn()
        {
            (int r, int c) = Bot.MakeMove();

            if (PlayerBoard.Hidden_PlayerBoard[r, c] == 'S')
            {
                PlayerBoard.Hidden_PlayerBoard[r, c] = 'X';

                if (IsShipSunk(PlayerBoard.Hidden_PlayerBoard))
                {
                    playerShips--;
                    Bot.ProcessShotResult(r, c, 'H', true);
                }
                else
                {
                    Bot.ProcessShotResult(r, c, 'H', false);
                }
            }
            else if (PlayerBoard.Hidden_PlayerBoard[r, c] == '~')
            {
                PlayerBoard.Hidden_PlayerBoard[r, c] = 'O';
                Bot.ProcessShotResult(r, c, 'M', false);
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
