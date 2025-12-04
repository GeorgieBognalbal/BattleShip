using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShip
{
    public class Bot
    {
        public char[,] hidden_BotBoard = new char[10, 10];
        public char[,] display_BotBoard = new char[10, 10];


        private int _lastHitRow = -1;
        private int _lastHitCol = -1;
        private bool _isHunting = true;

        private readonly Random _random = new Random();
        private static readonly int[] ShipSizes = { 5, 4, 3, 3, 2 };

        public void InitializeBoards()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    hidden_BotBoard[i, j] = '~';
                    display_BotBoard[i, j] = '~';
                }
            }
        }

        public void PlaceShips()
        {
                bool placed = false;
                while (!placed)
                {
                    bool isHorizontal = _random.Next(2) == 0;

                    int maxRow = isHorizontal ? 10 : 10 - size;
                    int maxCol = isHorizontal ? 10 - size : 10;

                    int startRow = _random.Next(maxRow);
                    int startCol = _random.Next(maxCol);

                    if (IsValidPlacement(startRow, startCol, size, isHorizontal))
                    {
                        for (int i = 0; i < size; i++)
                        {
                            int currentRow = startRow + (isHorizontal ? 0 : i);
                            int currentCol = startCol + (isHorizontal ? i : 0);
                            hidden_BotBoard[currentRow, currentCol] = 'S';
                        }
                        placed = true;
                    }
                }
            }
        }
        private bool IsValidPlacement(int r, int c, int size, bool isHorizontal)
        {
            for (int i = 0; i < size; i++)
            {
                int currentRow = r + (isHorizontal ? 0 : i);
                int currentCol = c + (isHorizontal ? i : 0);

                if (hidden_BotBoard[currentRow, currentCol] == 'S')
                {
                    return false;
                }
            }
            return true;

        }
        public (int row, int col, char result) MakeMove(Board board)
        {
            (int row, int col) shot;

            if (_isHunting)
                shot = GetRandomValidShot();
            else
                shot = GetTargetShot();

            // Shoot at the board to get the result
            char result = board.ShootAt(shot.row, shot.col); // 'O' = hit, 'X' = miss

            // Process shot result for bot AI
            ProcessShotResult(shot.row, shot.col, result, false);

            return (shot.row, shot.col, result);
        }
        private (int, int) GetRandomValidShot()
        {

            int row, col;
            do
            {
                row = _random.Next(10);
                col = _random.Next(10);

            } while (display_BotBoard[row, col] != '~');

            return (row, col);
        }
        private (int, int) GetTargetShot()
        {
            (int dr, int dc)[] directions = { (-1, 0), (1, 0), (0, -1), (0, 1) };

            foreach (var (dr, dc) in directions)
            {
                int r = _lastHitRow + dr;
                int c = _lastHitCol + dc;

                if (r >= 0 && r < 10 && c >= 0 && c < 10)
                {
                    if (display_BotBoard[r, c] == '~')
                    {
                        return (r, c);
                    }
                }
            }
            return (-1, -1);
        }

        public void ProcessShotResult(int row, int col, char result, bool shipSunk)
        {
            display_BotBoard[row, col] = result;
            char hitMarker = 'O';
            char missMarker = 'X';

            if (result == hitMarker)
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
            else if (result == missMarker)
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