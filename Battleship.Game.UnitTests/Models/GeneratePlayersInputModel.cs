using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.UnitTests.Models
{
    public class GeneratePlayersInputModel
    {
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }
        public int NoOfComputerPlayers { get; set; }
    }
}
