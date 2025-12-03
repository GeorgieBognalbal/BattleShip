using System;

namespace BattleShip
{
    public class Board
    {
        public char[,] Hidden_PlayerBoard { get; private set; }
        public char[,] Hidden_BotBoard { get; private set; }
        public char[,] Display_BotBoard { get; private set; }
        public char[,] Display_PlayerBoard { get; private set; }
        public int Size { get; private set; } = 10;

        public void InitializeBoard()
        {
            Size = 10;
            Hidden_PlayerBoard = new char[Size, Size];
            Hidden_BotBoard = new char[Size, Size];
            Display_PlayerBoard = new char[Size, Size];
            Display_BotBoard = new char[Size, Size];

            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                {
                    Hidden_PlayerBoard[r, c] = '~';
                    Hidden_BotBoard[r, c] = '~';
                    Display_PlayerBoard[r, c] = '~';
                    Display_BotBoard[r, c] = '~';
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

        //============================ Sync Bot Display =============================
        public void SyncBotBoard()
        {
            Console.WriteLine(@"      
       A   B   C   D   E   F   G   H   I   J  
     ╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗");

            for (int i = 0; i < 10; i++)
            {
                Console.Write((i + 1).ToString().PadLeft(2) + "   ║");

                for (int j = 0; j < 10; j++)
                {
                    char cell = Display_PlayerBoard[i, j] = '~';

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
                }
                Console.WriteLine("║");

                if (i < 9)
                    Console.WriteLine("     ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣");
            }

            Console.WriteLine("     ╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝");
        }
        //============================ Side by Side Display ==========================
        public static class BoardDisplay
        {
            public static void ShowSideBySide(Board board)
            {
                int size = board.Size;

                char[,] P = board.Hidden_PlayerBoard;
                char[,] B = board.Hidden_BotBoard;

                Console.WriteLine(@"
       A   B   C   D   E   F   G   H   I   J             A   B   C   D   E   F   G   H   I   J
     ╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗         ╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗");

                for (int r = 0; r < size; r++)
                {
                    Console.Write((r + 1).ToString().PadLeft(2) + "   ║");

                    for (int c = 0; c < size; c++)
                    {
                        char cell = B[r, c] = '~';
                        Console.Write(" " + cell + " ");
                        if (c < size - 1) Console.Write("║");
                    }

                    Console.Write("║    ");

                    Console.Write((r + 1).ToString().PadLeft(2) + "   ║");

                    for (int c = 0; c < size; c++)
                    {
                        char cell = P[r, c] = '~';
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

        //============================ Placement Support =============================
        public bool CanPlaceShip(int row, int col, int length, Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                if (col + length > Size) return false;

                for (int i = 0; i < length; i++)
                    if (Hidden_PlayerBoard[row, col + i] != '~') 
                        
                return false;
            }
            else
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
