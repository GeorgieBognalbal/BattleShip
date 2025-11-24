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
        public char[,] HiddenBotBoard { get; private set; }
        public int Size { get; private set; } = 10;

        public void BotBoard()
        {
            HiddenBotBoard = new char[Size, Size];

            // Fill with water
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    HiddenBotBoard[r, c] = '~';
                }
            }
        }

        // Draw the grid
        public void Draw(bool showShips)
        {
            Console.WriteLine(@"     
       A   B   C   D   E   F   G   H   I   J
     ╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗");

            for (int i = 0; i < 10; i++)
            {
                Console.Write((i + 1).ToString().PadLeft(2) + "   ║");

                for (int j = 0; j < 10; j++)
                {
                    char cell = HiddenBotBoard[i, j];

                    if (!showShips && cell == 'S')
                        cell = '~';

                    Console.Write(" " + cell + " ");

                    if (j < 9) Console.Write("║");
                }

                Console.WriteLine("║");

                if (i < 9)
                    Console.WriteLine("     ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣");
            }

            Console.WriteLine("     ╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝");
        }

        //==========================================================================================================================
        //==========================================HIDE THE SHIPS IN THE TRUE ARRAY================================================
        public char[,] BotShipCoverDisplay = new char[10, 10];

        public void DrawHiddenBoard(bool HideShips)
        {
            Console.WriteLine(@"      
       A   B   C   D   E   F   G   H   I   J
     ╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗");

            for (int i = 0; i < 10; i++)
            {
                Console.Write((i + 1).ToString().PadLeft(2) + "   ║");

                for (int j = 0; j < 10; j++)
                {
                    char cell = BotShipCoverDisplay[i, j] = '~';

                    if (HiddenBotBoard[i, j] == 'S')
                    {
                        cell = '~';
                    }
                    else if (HiddenBotBoard[i, j] == 'O')
                    {
                        cell = 'O';
                    }
                    else if (HiddenBotBoard[i, j] == 'X')
                    {
                        cell = 'X';
                    }

                    Console.Write(" " + cell + " ");

                    if (j < 9) Console.Write("║");
                }

                Console.WriteLine("║");

                if (i < 9)
                    Console.WriteLine("     ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣");
            }

            Console.WriteLine("     ╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝");

        }
        //==========================================================================================================================
        //==========================================================================================================================
    }
}
