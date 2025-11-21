namespace BattleShip
{
        public enum Orientation
        {
            Horizontal,
            Vertical
        }
    public class Ship
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public Orientation Direction { get; set; }

        public Ship(string name, int length, Orientation direction)
        {
            Name = name;
            Length = length;
            Direction = direction;
        }
    }
}
