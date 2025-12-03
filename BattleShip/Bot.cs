using System;
using System.Collections.Generic;

namespace BattleShip
{
    public class Bot
    {
        private Random random = new Random();
        private bool[,] shotsFired = new bool[10, 10];

        // Reference to the bot's board (ships placed)
        public char[,] Hidden_BotBoard { get; private set; } = new char[10, 10];

        public void PlaceShips()
        {
            // Simple random placement of ships for now
            var ships = new List<Ship>()
            {
                new Ship("Destroyer", 2, Orientation.Horizontal),
                new Ship("Submarine", 3, Orientation.Horizontal),
                new Ship("Cruiser", 3, Orientation.Horizontal),
                new Ship("Battleship", 4, Orientation.Horizontal),
                new Ship("Carrier", 5, Orientation.Horizontal)
            };

            foreach (var ship in ships)
            {
                bool placed = false;
                while (!placed)
                {
                    int row = random.Next(0, 10);
                    int col = random.Next(0, 10);
                    Orientation orientation = (Orientation)random.Next(0, 2);

                    if (CanPlaceShip(Hidden_BotBoard, ship.Size, row, col, orientation))
                    {
                        PlaceShip(Hidden_BotBoard, ship.Size, row, col, orientation);
                        placed = true;
                    }
                }
            }
        }

        private bool CanPlaceShip(char[,] board, int size, int row, int col, Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                if (col + size > 10) return false;
                for (int i = 0; i < size; i++)
                    if (board[row, col + i] == 'S') return false;
            }
            else
            {
                if (row + size > 10) return false;
                for (int i = 0; i < size; i++)
                    if (board[row + i, col] == 'S') return false;
            }
            return true;
        }

        private void PlaceShip(char[,] board, int size, int row, int col, Orientation orientation)
        {
            for (int i = 0; i < size; i++)
            {
                if (orientation == Orientation.Horizontal)
                    board[row, col + i] = 'S';
                else
                    board[row + i, col] = 'S';
            }
        }

        public (int row, int col, char result) MakeMove(char[,] playerBoard)
        {
            int row, col;
            do
            {
                row = random.Next(0, 10);
                col = random.Next(0, 10);
            } while (shotsFired[row, col]);

            shotsFired[row, col] = true;

            char result;
            if (playerBoard[row, col] == 'S')
            {
                playerBoard[row, col] = 'X';
                result = 'O';
            }
            else
            {
                playerBoard[row, col] = '*';
                result = '*';
            }

            return (row, col, result);
        }

        public void ResetShots()
        {
            shotsFired = new bool[10, 10];
        }
    }
}
