using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShip
{
    internal class Bot
    {

        private readonly Random _random = new Random();
        private static readonly int[] ShipSizes = { 5, 4, 3, 3, 2 };

        public void PlaceShips()
        {
            foreach (int size in ShipSizes)
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
    }
}




