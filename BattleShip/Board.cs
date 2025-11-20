using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    internal class Board
    {
        char[,] grid = new char[10, 10];

        public void DisplayBoard()
        {
            Console.WriteLine(@"     
     A B C D E F G H I J
     -------------------");
            for (int i = 0; i < 10; i++)
            {
                Console.Write((i + 1).ToString().PadLeft(2) + " | ");
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(grid[i, j] + "~ ");
                }
                Console.WriteLine();
            }
        }
    }
}
