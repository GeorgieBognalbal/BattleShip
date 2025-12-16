using BattleShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class Bot
    {
        public Board Board;

        public readonly Random _random = new Random();
        public readonly bool[,] shotsFired = new bool[10, 10];

        public bool _isHunting = true;
        public int _lastHitRow = -1;
        public int _lastHitCol = -1;

        // Reference to the bot's board (ships placed)
        public char[,] Hidden_BotBoard = new char[10, 10];

        // Method to place ships RANDOMLY on the bot's board
        public void PlaceShips(Board board = null)
        {
            if (board != null)
            {
                this.Board = board;
            }

            int gridSize = Hidden_BotBoard.GetLength(0);

            // Initialize board to '.' for empty
            for (int r = 0; r < gridSize; r++)
            {
                for (int c = 0; c < gridSize; c++)
                {
                    Hidden_BotBoard[r, c] = '.';
                }
            }

            // Ship sizes (Destroyer=2, Submarine=3, Cruiser=3, Battleship=4, Carrier=5)
            var shipSizes = new List<int> { 2, 3, 3, 4, 5 };

            foreach (int shipSize in shipSizes)
            {
                bool placed = false;
                int attempts = 0;
                while (!placed && attempts < 1000)
                {
                    attempts++;
                    int r = _random.Next(0, gridSize);
                    int c = _random.Next(0, gridSize);
                    Orientation orientation = (Orientation)_random.Next(0, 2); // 0 or 1 for Horizontal/Vertical

                    // check if ship can be placed
                    if (CanPlaceShip(Hidden_BotBoard, shipSize, r, c, orientation))
                    {
                        bool isHorizontal = orientation == Orientation.Horizontal;
                        for (int i = 0; i < shipSize; i++)
                        {
                            int currentRow = r + (isHorizontal ? 0 : i);
                            int currentCol = c + (isHorizontal ? i : 0);
                            Hidden_BotBoard[currentRow, currentCol] = 'S';
                        }
                        placed = true;
                    }
                }

                // If we failed to place after many attempts, throw to surface the issue.
                if (!placed)
                {
                    throw new InvalidOperationException($"Failed to place ship of size {shipSize} after many attempts.");
                }
            }
        }

        // method to check if coordinates are valid for ship placement
        public bool CanPlaceShip(char[,] board, int shipSize, int row, int col, Orientation orientation)
        {
            int gridSize = board.GetLength(0);
            bool isHorizontal = orientation == Orientation.Horizontal;

            // Check bounds
            if (isHorizontal)
            {
                if (col + shipSize > gridSize) return false;
            }
            else
            {
                if (row + shipSize > gridSize) return false;
            }

            // Check overlap
            for (int i = 0; i < shipSize; i++)
            {
                int currentRow = row + (isHorizontal ? 0 : i);
                int currentCol = col + (isHorizontal ? i : 0);

                if (board[currentRow, currentCol] == 'S')
                {
                    return false;
                }
            }

            return true;
        }

        // afer checking everything, bot make a move
        public (int row, int col) MakeMove(Board playerBoard)
        {
            (int row, int col) shot;

            if (_isHunting)
            {
                shot = GetRandomValidShot();
            }
            else
            {
                shot = GetTargetShot();

                if (shot.row == -1)
                {
                    _isHunting = true;
                    shot = GetRandomValidShot();
                }
            }

            // Mark shot as fired to avoid repeats
            if (shot.row >= 0 && shot.col >= 0 && shot.row < shotsFired.GetLength(0) && shot.col < shotsFired.GetLength(1))
            {
                shotsFired[shot.row, shot.col] = true;
            }

            return shot;
        }

        public (int, int) GetRandomValidShot()
        {
            int gridSize = shotsFired.GetLength(0);
            // Try random picks first
            for (int attempt = 0; attempt < 200; attempt++)
            {
                int row = _random.Next(gridSize);
                int col = _random.Next(gridSize);

                if (!shotsFired[row, col] && IsUnknownOnDisplay(row, col))
                {
                    return (row, col);
                }
            }

            // Just incase it fails to find a random valid shot, do a full scan
            for (int r = 0; r < gridSize; r++)
            {
                for (int c = 0; c < gridSize; c++)
                {
                    if (!shotsFired[r, c] && IsUnknownOnDisplay(r, c))
                    {
                        return (r, c);
                    }
                }
            }

            // if valid shot found
            return (-1, -1);
        }

        public (int, int) GetTargetShot()
        {
            if (_lastHitRow < 0 || _lastHitCol < 0) return (-1, -1);

            (int dr, int dc)[] directions = { (-1, 0), (1, 0), (0, -1), (0, 1) };

            int gridSize = shotsFired.GetLength(0);

            foreach (var (dr, dc) in directions)
            {
                int r = _lastHitRow + dr;
                int c = _lastHitCol + dc;

                if (r >= 0 && r < gridSize && c >= 0 && c < gridSize)
                {
                    if (!shotsFired[r, c] && IsUnknownOnDisplay(r, c))
                    {
                        return (r, c);
                    }
                }
            }
            return (-1, -1);
        }

        public bool IsUnknownOnDisplay(int row, int col)
        {
            if (this.Board != null)
            {
                try
                {
                    var display = this.Board.Display_PlayerBoard;
                    if (display != null && display.GetLength(0) > row && display.GetLength(1) > col)
                    {
                        return display[row, col] == '~';
                    }
                }
                catch
                {
                    // another fallback just incase of any error
                }
            }
            return !shotsFired[row, col];
        }

        // this is updates the bot after each shot
        public void ProcessShotResult(int row, int col, char result, bool shipSunk)
        {
            // Mark shot as fired locally
            if (row >= 0 && row < shotsFired.GetLength(0) && col >= 0 && col < shotsFired.GetLength(1))
            {
                shotsFired[row, col] = true;
            }

            // Update hunting state if hit
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
    }
}
