using Battleship.Game.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Interfaces
{
    public interface IUtilService
    {
        Cell RandomPosition(IList<Cell> freeCells);

        Cell ReceiveInput(Dimension boardDimension);
    }
}
