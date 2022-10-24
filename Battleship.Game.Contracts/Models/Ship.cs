using System;

namespace Battleship.Game.Contracts.Models
{
    public class Ship
    {
        public string Id { get; set; }
        public Dimension Dimension { get; set; }

        public int HitsConsumed { get; set; }

        public Ship(int length, int width)
        {
            Id = Guid.NewGuid().ToString();
            Dimension = new Dimension(width, length);
        }

        public bool IsSunk()
        {
            var dimensions = Dimension.Width * Dimension.Height;
            return dimensions == HitsConsumed;
        }
    }
}
