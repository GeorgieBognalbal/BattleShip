using System;

namespace BattleShip
{
    public class Board
    {
        public char[,] Hidden { get; private set; }
        public int Size { get; private set; } = 10;

        public Board()
        {
            Hidden = new char[Size, Size];

            // Fill with water
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    Hidden[r, c] = '~';
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
                    char cell = Hidden[i, j];

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
        public char[,] ShipCoverDisplay = new char[10, 10];

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
                    char cell = ShipCoverDisplay[i, j] = '~';

                    if (Hidden[i, j] == 'S')
                    {
                        cell = '~';
                    } 
                    else if (Hidden[i, j] == 'O') 
                    {
                        cell = 'O';
                    }
                    else if (Hidden[i, j] == 'X')
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

        public bool CanPlaceShip(int row, int col, int length, Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                if (col + length > Size) return false;

                for (int i = 0; i < length; i++)
                    if (Hidden[row, col + i] != '~') return false;
            }
            else // Vertical
            {
                if (row + length > Size) return false;

                for (int i = 0; i < length; i++)
                    if (Hidden[row + i, col] != '~') return false;
            }
            return true;
        }

        public void PlaceShip(int row, int col, int length, Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                for (int i = 0; i < length; i++)
                    Hidden[row, col + i] = 'S';
            }
            else
            {
                for (int i = 0; i < length; i++)
                    Hidden[row + i, col] = 'S';
            }
        }


    }
}
