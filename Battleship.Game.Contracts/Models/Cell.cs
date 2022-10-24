using Battleship.Game.Contracts.Enums;
using System;

namespace Battleship.Game.Contracts.Models
{
    public class Cell : IEquatable<Cell>
    {
        public Ship Ship { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public CellStateType State { get; set; }

        public Cell(Ship ship, int row, int col)
        {
            Ship = ship;
            Row = row;
            Col = col;
            State = CellStateType.Empty;
        }

        public bool Equals(Cell other)
        {
            bool isEqual = false;
            if (other != null)
            {
                isEqual = other.Row == this.Row && other.Col == this.Col;
            }
            return isEqual;
        }
    }
}
