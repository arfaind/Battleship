using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Contracts.Models
{
    public class Player
    {
        public bool IsComputer { get; set; }
        public string Id { get; set; }
        public Board Board { get; set; }
    }
}
