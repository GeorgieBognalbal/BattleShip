using System;

namespace BattleShip
{
    public class Board
    {
        public char[,] Hidden_PlayerBoard { get; private set; }
        public char[,] hidden_BotBoard { get; private set; }
        public char[,] display_BotBoard { get; private set; }
        public int Size { get; private set; } = 10;

        public Board()
        {
            Hidden_PlayerBoard = new char[Size, Size];
            hidden_BotBoard = new char[Size, Size];

            // Fill with water
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    Hidden_PlayerBoard[r, c] = '~';
                    hidden_BotBoard[r, c] = '~';
                }
            }
        }

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

        //==========================================================================================================================
        //==========================================PLACEMENT STAGE BOARD===========================================================
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
                    char cell = Hidden_PlayerBoard[i, j];

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

                    if (Hidden_PlayerBoard[i, j] == 'S')
                    {
                        cell = '~';
                    } 
                    else if (Hidden_PlayerBoard[i, j] == 'O') 
                    {
                        cell = 'O';
                    }
                    else if (Hidden_PlayerBoard[i, j] == 'X')
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
//=============================================SIDE BY SIDE BOARD===========================================================


        public static class BoardDisplay
        {
            public static void ShowSideBySide(Board board)
            {
                int size = board.Size;

                char[,] A = board.Hidden_PlayerBoard;
                char[,] B = board.hidden_BotBoard;

                Console.WriteLine(@"
       A   B   C   D   E   F   G   H   I   J             A   B   C   D   E   F   G   H   I   J
     ╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗         ╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗");

                for (int r = 0; r < size; r++)
                {
                    
                    Console.Write((r + 1).ToString().PadLeft(2) + "   ║");

                    for (int c = 0; c < size; c++)
                    {
                        char cell = A[r, c];
                        if (cell == 'S') cell = '~'; 
                        Console.Write(" " + cell + " ");
                        if (c < size - 1) Console.Write("║");
                    }

                    Console.Write("║    ");

                    Console.Write((r + 1).ToString().PadLeft(2) + "   ║");

                    for (int c = 0; c < size; c++)
                    {
                        char cell = B[r, c];
                        if (cell == 'S') cell = '~';
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


        public bool CanPlaceShip(int row, int col, int length, Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                if (col + length > Size) 
            return false;

                for (int i = 0; i < length; i++)
                    if (Hidden_PlayerBoard[row, col + i] != '~') 
                        
                return false;
            }
            else // Vertical
            {
                if (row + length > Size) return false;

                for (int i = 0; i < length; i++)
                    if (Hidden_PlayerBoard[row + i, col] != '~') 
               
                return false;
            }
            return true;
        }

        public void PlaceShip(int row, int col, int length, Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                for (int i = 0; i < length; i++)
                    Hidden_PlayerBoard[row, col + i] = 'S';
            }
            else
            {
                for (int i = 0; i < length; i++)
                    Hidden_PlayerBoard[row + i, col] = 'S';
            }
        }


    }
}
