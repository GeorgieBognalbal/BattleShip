using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShip
{
    internal class BattleLogic
    {
        public void Player1(Board board)
        {
            while (true)
            {
                // Row
                Console.Write("Row (1-10): ");
                if (!int.TryParse(Console.ReadLine(), out int row) || row < 1 || row > 10)
                    continue;

                int r = row - 1;

                // Column
                Console.Write("Column (A-J): ");
                string colInput = Console.ReadLine().ToUpper();
                if (colInput.Length != 1 || colInput[0] < 'A' || colInput[0] > 'J')
                    continue;

                //Condition for hit and miss
                int c = colInput[0] - 'A';

                if (board.Hidden[r, c] == '~') // tinitignan kung may ship or empty sa hidden array
                {
                    board.ShipCoverDisplay[r, c] = 'O'; // Miss
                    Console.WriteLine("MISS");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else if (board.Hidden[r, c] == 'S') // kung may ship - Hit, tas update ship cover display
                {
                    board.ShipCoverDisplay[r, c] = 'X'; // Hit
                    Console.WriteLine("HIT!");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else { Console.WriteLine("dito"); }

                if (board.ShipCoverDisplay[r, c] == 'X' || board.ShipCoverDisplay[r, c] == 'O')
                {
                    Console.WriteLine("You have already fired at this location. Try again.");
                    Console.Clear();
                    continue;
                }
            }
        }

        public void Player2(Board board)
        {
            while (true)
            {
                // Row
                Console.Write("Row (1-10): ");
                if (!int.TryParse(Console.ReadLine(), out int row) || row < 1 || row > 10)
                    continue;

                int r = row - 1;

                // Column
                Console.Write("Column (A-J): ");
                string colInput = Console.ReadLine().ToUpper();
                if (colInput.Length != 1 || colInput[0] < 'A' || colInput[0] > 'J')
                    continue;

                int c = colInput[0] - 'A';

            }
        }
    }
}
