using BattleShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using System.Threading;

namespace BattleShip
{
    public class Board
    {
        Design design = new Design();

        public char[,] Hidden_PlayerBoard;
        public char[,] Hidden_BotBoard;
        public char[,] Display_BotBoard;
        public char[,] Display_PlayerBoard;
        public int Size = 10;

        public void InitializeBoard()
        {
            Hidden_PlayerBoard = new char[Size, Size];
            Display_PlayerBoard = new char[Size, Size];

            Hidden_BotBoard = new char[Size, Size];
            Display_BotBoard = new char[Size, Size];


            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    Hidden_PlayerBoard[r, c] = '~';
                    Display_PlayerBoard[r, c] = '~';
                    Hidden_BotBoard[r, c] = '~';
                    Display_BotBoard[r, c] = '~';
                }
            }
        }
        //PLACEMENT STAGE BOARD=======================================================
        public void Draw(bool showShips)
        {
            int x = 2, y = 1;
            Console.SetCursorPosition(x, y);
            Console.WriteLine("                                  A   B   C   D   E   F   G   H   I   J  ");
            Console.WriteLine("                                  ╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗");

            for (int i = 0; i < 10; i++)
            {
                
                Console.Write("                              " + (i + 1).ToString().PadLeft(2) + "  ║");

                for (int j = 0; j < 10; j++)
                {
                    char cell = Hidden_PlayerBoard[i, j];

                    if (!showShips && cell == 'S')
                        cell = '~';

                    Console.Write(" " + cell + " ");

                    if (j < 9) Console.Write("║");
                }

                Console.WriteLine("║");

                if (i < 9)
                    Console.WriteLine("                                  ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣");


            }

            Console.WriteLine("                                  ╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝");

        }

        //Side by Side Display =======================================================
        public static class BoardDisplay
        {
            public static void ShowSideBySide(Board board)
            {
                int size = board.Size;

                Console.WriteLine(@"
       A   B   C   D   E   F   G   H   I   J             A   B   C   D   E   F   G   H   I   J
     ╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗         ╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗");

                for (int r = 0; r < size; r++)
                {
                    Console.Write((r + 1).ToString().PadLeft(2) + "   ║");
                    // LEFT GRID — what the player sees of the bot
                    for (int c = 0; c < size; c++)
                    {
                        char cell = board.Display_BotBoard[r, c];
                        Console.Write(" " + cell + " ");
                        if (c < size - 1) Console.Write("║");
                    }

                    Console.Write("║    ");
                    Console.Write((r + 1).ToString().PadLeft(2) + "   ║");

                    // RIGHT GRID — what the bot sees of the player
                    for (int c = 0; c < size; c++)
                    {
                        char cell = board.Display_PlayerBoard[r, c];
                        Console.Write(" " + cell + " ");
                        if (c < size - 1) Console.Write("║");
                    }
                    Console.WriteLine("║");

                    if (r < size - 1)
                    {
                        Console.WriteLine("     ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣         ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣");
                    }
                }

                Console.WriteLine("     ╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝         ╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝");
            }
        }

    }

}