using BattleShip;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BattleShip
{
    public class Placement
    {
        private readonly Board _board;

        public Placement(Board board)
        {
            _board = board ?? throw new ArgumentNullException(nameof(board));
        }

        //================= Placement Support =================
        public bool CanPlaceShip(int row, int col, int length, Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                if (col + length > _board.Size) return false;

                for (int i = 0; i < length; i++)
                    if (_board.Hidden_PlayerBoard[row, col + i] != '~') return false;
            }
            else
            {
                if (row + length > _board.Size) return false;

                for (int i = 0; i < length; i++)
                    if (_board.Hidden_PlayerBoard[row + i, col] != '~') return false;
            }
            return true;
        }

        public void PlaceShip(int row, int col, int length, Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                for (int i = 0; i < length; i++)
                    _board.Hidden_PlayerBoard[row, col + i] = 'S';
            }
            else
            {
                for (int i = 0; i < length; i++)
                    _board.Hidden_PlayerBoard[row + i, col] = 'S';
            }
        }

        public char ShootAt(int row, int col)
        {
            if (_board.Hidden_BotBoard[row, col] == 'S')
            {
                _board.Hidden_BotBoard[row, col] = 'X';     // mark hidden as hit
                _board.Display_BotBoard[row, col] = 'X';    // mark display as hit
                return 'X';
            }
            else if (_board.Hidden_BotBoard[row, col] == '~')
            {
                _board.Display_BotBoard[row, col] = 'O';    // mark display as miss
                return 'O';
            }
            else
            {
                return _board.Display_BotBoard[row, col];   // already shot
            }
        }

        //================= Placement Manager =================
        public class PlacementManager
        {
            private readonly Placement _placement;
            private readonly Design _design;

            public PlacementManager(Placement placement, Design design)
            {
                _placement = placement ?? throw new ArgumentNullException(nameof(placement));
                _design = design ?? throw new ArgumentNullException(nameof(design));
            }

            public bool IsValidInput(string input)
            {
                if (string.IsNullOrWhiteSpace(input)) return false;
                if (input.Length < 2) return false;
                if (input[0] < 'A' || input[0] > 'J') return false;

                if (!int.TryParse(input.Substring(1), out int r)) return false;
                if (r < 1 || r > 10) return false;

                return true;
            }

            public void PlaceAllShips(Board board, List<Ship> ships)
            {
                foreach (var ship in ships)
                {
                    PlaceSingleShip(board, ship);
                }
            }

            public void PlaceSingleShip(Board board, Ship ship)
            {
                Console.CursorVisible = true;

                while (true)
                {
                    Console.Clear();
                    _design.PlacementBoader();
                    //_design.PlacementStage_Header();
                    board.Draw(true);

                    Console.WriteLine($"│ ┌───────────────────────────────────────────┐");
                    Console.WriteLine($"│ │Placing {ship.Name} (length {ship.Length})               │"); 
                    Console.WriteLine($"│ │Enter Coordinate (A1):                     │");
                    Console.WriteLine($"│ │Orientation (H/V)    :                     │");
                    Console.WriteLine($"│ └───────────────────────────────────────────┘");

                    Console.SetCursorPosition(26, 25);
                    string input = Console.ReadLine()?.ToUpper() ?? string.Empty;

                    if (!IsValidInput(input))
                    {
                        Console.WriteLine("│  Invalid input!");
                        Console.ReadKey(true);
                        continue;
                    }

                    int col = input[0] - 'A';
                    int row = int.Parse(input.Substring(1)) - 1;

                    Console.SetCursorPosition(26, 26);
                    string o = (Console.ReadLine() ?? "").Trim().ToUpper();
                    ship.Direction = (o == "V") ? Orientation.Vertical : Orientation.Horizontal;

                    if (_placement.CanPlaceShip(row, col, ship.Length, ship.Direction))
                    {
                        _placement.PlaceShip(row, col, ship.Length, ship.Direction);
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
}