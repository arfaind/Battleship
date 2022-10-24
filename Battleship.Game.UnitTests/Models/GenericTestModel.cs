using Battleship.Game.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.UnitTests.Models
{
    internal class GenericTestModel<X,Y>
    {
        public X Input { get; set; }
        public Y Output { get; set; }
    }
}
