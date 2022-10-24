using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Contracts.Models
{
    public class Dice
    {
        public (int row, int col) Roll(Dimension maxDimensions)
        {
            var row = 0;
            var col = 0;
            return (row, col);
        }
    }
}
