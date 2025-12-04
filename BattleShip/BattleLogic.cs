using System;
using System.Diagnostics.Eventing.Reader;
using System.Threading;

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
            Board.SyncBotBoard(Bot.display_BotBoard);
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
                    Thread.Sleep(1000);
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
                } else {
                    Console.Clear();
                    Board.BoardDisplay.ShowSideBySide(board);
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
            char result = Board.ShootAt(row, col); // get 'O' for hit, 'X' for miss
            if (result == 'O')
            {
                Console.WriteLine("HIT!");
                Thread.Sleep(500);
                if (IsShipSunk(Board.Hidden_BotBoard))
                {
                    botShips--;
                    Console.WriteLine("You sunk a ship!");
                }
            }
            else
            {
                Console.WriteLine("MISS!");
                Thread.Sleep(500);
            }

            SyncBotDisplay();
        }

        //========================= BOT TURN =============================
        public void BotTurn()
        {
            Board board = Board;
            var (row, col, result) = Bot.MakeMove(board); // Bot now shoots and gets result

            if (result == 'O')
            {
                Console.WriteLine($"BOT hits at {row + 1},{col + 1}");
                if (IsShipSunk(Board.Hidden_PlayerBoard))
                {
                    playerShips--;
                    Console.WriteLine("BOT sunk one of your ships!");
                    Thread.Sleep(1000);
                }
            }
            else
            {
                Console.WriteLine($"BOT misses at {row + 1},{col + 1}");
                Thread.Sleep(1000);
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
