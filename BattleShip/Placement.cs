using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BattleShip
{
    public class PlacementManager
    {
        public void PlaceAllShips(Board board, List<Ship> ships)
        {
            foreach (var ship in ships)
            {
                PlaceSingleShip(board, ship);
            }
        }

        public void PlaceSingleShip(Board board, Ship ship)
        {
            while (true)
            {
                Console.Clear();
                board.PlacementStage_Header();
                board.Draw(true);

                Console.WriteLine($"\nPlacing {ship.Name} (length {ship.Length})");

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

                // Orientation
                Console.Write("Orientation (H/V): ");
                string o = Console.ReadLine().ToUpper();

                ship.Direction = (o == "V") ? Orientation.Vertical : Orientation.Horizontal;

                if (board.CanPlaceShip(r, c, ship.Length, ship.Direction))
                {
                    board.PlaceShip(r, c, ship.Length, ship.Direction);
                    Console.WriteLine("Placed!");
                    Console.ReadKey(true);
                    break;
                }
                else
                {
                    Console.WriteLine("Cannot place ship there.");
                    Console.ReadKey(true);
                }
            }
        }
    }
}
