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
        private void Fire(Board board, int r, int c)
        {
            if (board.Hidden[r, c] == '~')
            {
                board.Hidden[r, c] = 'O';
                board.ShipCoverDisplay[r, c] = 'O';
                Console.WriteLine("MISS");
            }
            else if (board.Hidden[r, c] == 'S')
            {
                board.Hidden[r, c] = 'X';
                board.ShipCoverDisplay[r, c] = 'X';
                Console.WriteLine("HIT!");
            }
            else if (board.Hidden[r, c] == 'X' || board.Hidden[r, c] == 'O')
            {
                Console.WriteLine("You already fired here. Try again.");
            }
        }


        public void Player1(Board board)
        {
            while (true)
            {
                Console.Clear();
                board.header_Player1();
                board.DrawHiddenBoard(true);

                Console.Write("Row (1-10): ");
                if (!int.TryParse(Console.ReadLine(), out int row) || row < 1 || row > 10)
                    continue;

                int r = row - 1;

                Console.Write("Column (A-J): ");
                string colInput = Console.ReadLine().ToUpper();
                if (colInput.Length != 1 || colInput[0] < 'A' || colInput[0] > 'J')
                    continue;

                int c = colInput[0] - 'A';

                Fire(board, r, c);

                Thread.Sleep(1000);
            }
        }
    }
}
