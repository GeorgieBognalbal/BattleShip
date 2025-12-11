using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip_v2
{
    public enum Orientation
    {
        Horizontal,
        Vertical
    }
    public class Ship
    {
        public string Name;
        public int Length;
        public Orientation Direction;

        public Ship(string name, int length, Orientation direction)
        {
            Name = name;
            Length = length;
            Direction = direction;
        }
    }
}
