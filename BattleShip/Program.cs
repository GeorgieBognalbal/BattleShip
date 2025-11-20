using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Interface menu = new Interface();
            Board gameBoard = new Board();

            while (true)
            try
            {
                menu.MainMenu();
                Console.CursorVisible = false;
                var key = Console.ReadKey();

                    switch (key.Key)
                    {
                        case ConsoleKey.Enter:
                            Console.Clear();
                            gameBoard.DisplayBoard();
                            Console.ReadKey();
                            if (Console.ReadKey().Key == ConsoleKey.Escape)
                            {
                                Console.Clear();
                                continue;
                            }
                            break;

                        case ConsoleKey.F1:
                            Console.Clear();
                            menu.Tutorial();
                            if (Console.ReadKey().Key == ConsoleKey.Escape)
                            {
                                Console.Clear();
                                continue;
                            }
                            break;

                        case ConsoleKey.Escape:
                            Environment.Exit(0);
                            break;
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
